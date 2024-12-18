using Elastic.Clients.Elasticsearch;
using Filter.WebApi.Consumers;
using Filter.WebApi.Services;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var settings = new ElasticsearchClientSettings(new Uri(builder.Configuration["Elasticsearch:Uri"] ?? ""))
    .DefaultIndex(builder.Configuration["Elasticsearch:Index"] ?? "");
var client = new ElasticsearchClient(settings);

builder.Services.AddSingleton(client);

builder.Services.AddScoped<IFilterService, FilterService>();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<ProductCreatedConsumer>();
    options.AddConsumer<ProductUpdatedConsumer>();
    options.AddConsumer<ProductDeletedConsumer>();

    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        config.ReceiveEndpoint("filter-product-created", endpoint =>
        {
            endpoint.ConfigureConsumer<ProductCreatedConsumer>(context);
        });

        config.ReceiveEndpoint("filter-product-updated", endpoint =>
        {
            endpoint.ConfigureConsumer<ProductUpdatedConsumer>(context);
        });

        config.ReceiveEndpoint("filter-product-deleted", endpoint =>
        {
            endpoint.ConfigureConsumer<ProductDeletedConsumer>(context);
        });
    });
});


builder.Services.AddSwaggerGen();//ekledim
builder.Services.AddEndpointsApiExplorer();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
/*    app.MapOpenApi()*/;
    app.UseSwagger();
  
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
