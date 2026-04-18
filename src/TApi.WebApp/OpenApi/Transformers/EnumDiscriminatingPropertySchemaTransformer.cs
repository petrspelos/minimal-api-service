using System.Text.Json.Nodes;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using TApi.WebApp.Features.Items.Domain;
using TApi.WebApp.Features.Items.Http;

namespace TApi.WebApp.OpenApi.Transformers;

public class EnumDiscriminatingPropertySchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(
        OpenApiSchema schema,
        OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type == typeof(ComplexItemDto))
        {
            ProcessComplexItemDto(schema);
        }

        return Task.CompletedTask;
    }

    private static void ProcessComplexItemDto(OpenApiSchema schema)
    {
        schema.Type = null;
        schema.Properties = null;
        schema.Required = null;
        schema.OneOf =
        [
            MakeVariant(ItemType.Draft.ToString(), []),
            MakeVariant(ItemType.NamedItem.ToString(), [nameof(ComplexItemDto.Name)]),
            MakeVariant(ItemType.FullItem.ToString(), [nameof(ComplexItemDto.Name), nameof(ComplexItemDto.Description)])
        ];
    }

    private static OpenApiSchema MakeVariant(string typeValue, string[] requiredProperties) => new()
    {
        Title = typeValue,
        Type = JsonSchemaType.Object,
        Properties = new Dictionary<string, IOpenApiSchema>
        {
            [nameof(ComplexItemDto.Type)] = new OpenApiSchema() { Type = JsonSchemaType.String, Enum = [JsonValue.Create(typeValue)] },
            [nameof(ComplexItemDto.Name)] = new OpenApiSchema() { Type = JsonSchemaType.String },
            [nameof(ComplexItemDto.Description)] = new OpenApiSchema() { Type = JsonSchemaType.String }
        },
        Required = new HashSet<string>([nameof(ComplexItemDto.Type), ..requiredProperties])
    };
}
