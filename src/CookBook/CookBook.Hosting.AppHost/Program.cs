var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CookBook_Api_App>("api")
    .WithOtlpExporter()
    .WithHttpEndpoint();

var web = builder.AddProject<Projects.CookBook_Web_App>("web")
    .WithOtlpExporter()
    .WithHttpEndpoint(targetPort: 8080, env: "HTTP_PORT");

builder.AddProject<Projects.CookBook_Hosting_Gateway>("gateway")
    .WithReference(api)
    .WithReference(web)
    .WithOtlpExporter()
    .WithHttpEndpoint()
    .WithExternalHttpEndpoints();

builder.Build().Run();
