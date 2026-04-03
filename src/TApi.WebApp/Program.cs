using Scalar.AspNetCore;
using TApi.WebApp.Configuration;
using TApi.WebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi(OpenApiConfigurator.Configure);
builder.Services.AddProblemDetails();
builder.Services.AddValidation();

// TODO: Implement Correlation ID headers (Correlate)

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(string.Empty);
}

var apiRoot = app.MapGroup("/api/v1/");

apiRoot.MapTestEndpoints();

app.Run();
