namespace Roblox.SystemEvents;

using System.Collections.Generic;

/// <summary>
/// Model for a paged result of <typeparamref name="T"/>
/// </summary>
public class PagedResultModel<T>
{
    /// <summary>
    /// Sets the count of items.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the page items.
    /// </summary>
    public ICollection<T> PageItems { get; set; }
}