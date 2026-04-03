namespace TApi.WebApp.Features.Items.Http;

public sealed class ItemListDto
{
    public required IReadOnlyCollection<ItemDto> Items { get; set; }
}
