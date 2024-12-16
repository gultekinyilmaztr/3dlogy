using Application;
using Base.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence.EntityConfigurations;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityURL"];
    opt.Audience = "ResourceCatalog";
});

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);//ekledim.
builder.Services.AddHttpContextAccessor();
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");
builder.Services.AddHttpContextAccessor();
//builder.Services.AddDistributedMemoryCache();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();//ekledim

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();//ekledim.
    app.UseSwaggerUI();//ekledim
}

//if (app.Environment.IsProduction())//ekledim.
app.ConfigureCustomExceptionMiddleware();//ekledim

app.MapDefaultEndpoints();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
