namespace TApi.WebApp.Configuration;

internal static class AuthorizationConfigurator
{
    internal static void AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();

        // NOTE: Default policy could be configured here,
        //       but would required AUTH even for Scalar UI.
        //       Disabled until exceptions can be configured.
        // var requireAuthPolicy = new AuthorizationPolicyBuilder()
        //     .RequireAuthenticatedUser()
        //     .Build();

        // builder.Services.AddAuthorizationBuilder()
        //     .SetFallbackPolicy(requireAuthPolicy);
    }
}
