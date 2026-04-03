using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TApi.WebApp.Configuration;

internal static class AuthenticationConfigurator
{
    internal static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => ConfigureJwtBearer(options, builder));
    }

    private static void ConfigureJwtBearer(JwtBearerOptions options, WebApplicationBuilder builder)
    {
        // TODO: Represent the Keycloak configuration entity.
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"]
            ?? throw new InvalidOperationException(
                "The 'Keycloak:MetadataAddress' configuration is required.");
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    }
}
