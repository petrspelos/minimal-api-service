using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace TApi.WebApp.Configuration;

internal static class HttpjsonOptionsConfigurator
{
    internal static void Configure(JsonOptions options)
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}
