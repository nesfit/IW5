var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CookBook_Api_App>("api")
    .WithOtlpExporter()
    .WithHttpEndpoint();

var web = builder.AddProject<Projects.CookBook_Web_App>("web")
    .WithOtlpExporter()
    .WithHttpEndpoint();

builder.AddProject<Projects.CookBook_Hosting_Gateway>("gateway")
    .WithOtlpExporter()
    .WithHttpEndpoint()
    .WithReference(api)
    .WithReference(web);

builder.Build().Run();
