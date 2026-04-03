using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using TApi.WebApp.Services.Authentication;

namespace TApi.WebApp.Configuration;

internal static class AuthenticationConfigurator
{
    internal static void AddAuthentication(this WebApplicationBuilder builder)
    {
        RegisterOpenIdDiscoveryServices(builder);
        RegisterJwtBearerAuthenticationServices(builder);
    }

    private static void RegisterOpenIdDiscoveryServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<OpenIdConfigurationProvider>();
        builder.Services.AddSingleton<IOpenIdConfigurationProvider>(sp =>
        {
            var inner = sp.GetRequiredService<OpenIdConfigurationProvider>();
            return new CachedOpenIdConfigurationProvider(inner);
        });
    }

    private static void RegisterJwtBearerAuthenticationServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => ConfigureJwtBearer(options, builder));
    }

    private static void ConfigureJwtBearer(JwtBearerOptions options, WebApplicationBuilder builder)
    {
        options.Authority = builder.Configuration["Keycloak:Authority"]
            ?? throw new InvalidOperationException(
                "The 'Keycloak:Authority' configuration is required.");

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
