using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHealthChecks();

builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityURL"];
    opt.Audience = "ResourceOcelot";
});

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json").Build();

builder.Services.AddOcelot(configuration);
var app = builder.Build();

app.MapHealthChecks("/health");
app.MapDefaultEndpoints();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();

public static class EndpointExtensions
{
    public static void MapDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}
