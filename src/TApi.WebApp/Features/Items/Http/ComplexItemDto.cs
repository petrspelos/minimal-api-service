using TApi.WebApp.Features.Items.Domain;

namespace TApi.WebApp.Features.Items.Http;

public sealed class ComplexItemDto
{
    public required ItemType Type { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
