using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Catalog_WebApi>("catalog-webapi");
builder.AddProject<Identity_API>("identity-webapi");

builder.AddProject<Projects.Filter_WebApi>("filter-webapi");

builder.Build().Run();
