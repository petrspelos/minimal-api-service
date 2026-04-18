namespace TApi.WebApp.Features.Items.Domain;

public enum ItemType
{
    /// <summary>
    /// Indicates that the item does require any additional fields.
    /// </summary>
    Draft,

    /// <summary>
    /// Indicates that the item only requires a name.
    /// </summary>
    NamedItem,

    /// <summary>
    /// Indicates that the item requires all fields.
    /// </summary>
    FullItem
}
