using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var prometheus = builder.AddContainer("prometheus", "prom/prometheus", "v2.53.2")
    .WithHttpEndpoint(targetPort: 9090, isProxied: false)
    .WithBindMount("./configs/prometheus/config.yaml", "/mnt/config/local-config.yaml", isReadOnly: true)
    .WithArgs("--config.file=/mnt/config/local-config.yaml", "--web.enable-remote-write-receiver");

var grafanaLoki = builder.AddContainer("grafanaLoki", "grafana/loki", "3.2.0")
    .WithHttpEndpoint(targetPort: 3100, isProxied: false)
    .WithBindMount("./configs/loki/config.yaml", "/mnt/config/local-config.yaml", isReadOnly: true)
    .WithArgs("-config.file=/mnt/config/local-config.yaml");

var grafanaTempo = builder.AddContainer("grafanaTempo", "grafana/tempo", "2.6.0")
    .WithHttpEndpoint(targetPort: 3200, isProxied: false)
    .WithEndpoint(targetPort: 9097, name: "grpc", isProxied: false)
    .WithBindMount("./configs/tempo/config.yaml", "/mnt/config/local-config.yaml", isReadOnly: true)
    .WithArgs("-config.file=/mnt/config/local-config.yaml");

var grafana = builder.AddContainer("grafana", "grafana/grafana", "11.2.0")
    .WithHttpEndpoint(targetPort: 3000)
    .WithBindMount("./configs/grafana/config", "/etc/grafana", isReadOnly: true)
    .WithBindMount("./configs/grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
    .WithVolume("grafana-data", "/var/lib/grafana")
    .WithReference(grafanaTempo.GetEndpoint("http"))
    .WithReference(prometheus.GetEndpoint("http"))
    .WithReference(grafanaLoki.GetEndpoint("http"));

var otelCollector = builder.AddContainer("otel-collector", "otel/opentelemetry-collector", "0.109.0")
    .WithEndpoint(targetPort: 4317, name: "grpc", scheme: "http", isProxied: false)
    .WithEndpoint(targetPort: 4318, name: "http", scheme: "http", isProxied: false)
    .WithBindMount("./configs/otel-collector/config.yaml", "/etc/otelcol/config.yaml", isReadOnly: true)
    .WithEnvironment("GRAFANA_TEMPO_GRPC", () =>
    {
        var endpoint = grafanaTempo.GetEndpoint("grpc");
        return $"{endpoint.ContainerHost}:{endpoint.TargetPort}";
    })
    .WithEnvironment(context =>
    {
        if (builder.Configuration.GetValue<string>("DOTNET_DASHBOARD_OTLP_ENDPOINT_URL") is {} otlpEndpoint)
        {
            var endpointUrl = new HostUrl(otlpEndpoint);
            context.EnvironmentVariables["DOTNET_DASHBOARD_OTLP_ENDPOINT"] = endpointUrl;
        }
    })
    .WithReference(grafanaTempo.GetEndpoint("grpc"))
    .WithReference(prometheus.GetEndpoint("http"))
    .WithReference(grafanaLoki.GetEndpoint("http"));

var api = builder.AddProject<Projects.CookBook_Api_App>("api")
    .WithHttpEndpoint();

var web = builder.AddProject<Projects.CookBook_Web_App>("web");

var gateway = builder.AddProject<Projects.CookBook_Hosting_Gateway>("gateway")
    .WithReference(api)
    .WithReference(web)
    .WithHttpEndpoint()
    .WithExternalHttpEndpoints();


if (builder.ExecutionContext.IsPublishMode)
{
    web.WithHttpEndpoint(targetPort: 8080, env: "HTTP_PORT");
}
else
{
    web.WithHttpEndpoint();
}

ConfigureOpenTelemetryExporter(api);
ConfigureOpenTelemetryExporter(gateway);

builder.Build().Run();
return;

void ConfigureOpenTelemetryExporter<T>(IResourceBuilder<T> resourceBuilder) where T : IResourceWithEnvironment
{
    if (builder.Configuration.GetValue<bool>("SendMetricsToCollector"))
    {
        var grpcEndpoint = otelCollector.GetEndpoint("grpc");
        resourceBuilder.WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT", grpcEndpoint);
        resourceBuilder.WithEnvironment("OTEL_EXPORTER_OTLP_PROTOCOL", "grpc");
    }
    else
    {
        // sends metrics to dashboard
        resourceBuilder.WithOtlpExporter();
    }
}
