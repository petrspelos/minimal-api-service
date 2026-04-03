using Scalar.AspNetCore;
using TApi.WebApp.Configuration;
using TApi.WebApp.Endpoints;
using TApi.WebApp.Services.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<OpenIdConfigurationProvider>();
builder.Services.AddSingleton<IOpenIdConfigurationProvider>(sp =>
{
    var inner = sp.GetRequiredService<OpenIdConfigurationProvider>();
    return new CachedOpenIdConfigurationProvider(inner);
});

builder.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi(OpenApiConfigurator.Configure);
builder.Services.AddProblemDetails();
builder.Services.AddValidation();

// TODO: Implement Correlation ID headers (Correlate)
// TODO: Authentication against Keycloak

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var apiRoot = app.MapGroup("/api/v1/");

apiRoot.MapTestEndpoints();

app.Run();
