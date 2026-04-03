using Scalar.AspNetCore;
using TApi.WebApp.Configuration;
using TApi.WebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(OpenApiConfigurator.ConfigureOpenApi);
builder.Services.AddProblemDetails();
builder.Services.AddValidation();

// TODO: Implement Correlation ID headers (Correlate)
// TODO: Authentication against Keycloak

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var apiRoot = app.MapGroup("/api/v1/");

apiRoot.MapTestEndpoints();

app.Run();
