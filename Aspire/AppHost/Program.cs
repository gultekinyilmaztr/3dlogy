using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Catalog_WebApi>("catalog-webapi");
builder.AddProject<Identity_API>("identity-webapi");

builder.Build().Run();
