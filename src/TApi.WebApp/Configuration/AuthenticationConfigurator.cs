using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TApi.WebApp.Configuration;

internal static class AuthenticationConfigurator
{
    internal static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
    }
}
