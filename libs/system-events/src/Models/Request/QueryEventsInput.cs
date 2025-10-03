namespace Roblox.SystemEvents;

using System;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Model for the input to the <see cref="QueryEventsOperation"/>
/// </summary>
public class QueryEventsInput
{
    /// <summary>
    /// Gets or sets the page.
    /// </summary>
    [FromQuery(Name = "page")]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    [FromQuery(Name = "pageSize")]

    public byte PageSize { get; set; }

    /// <summary>
    /// Gets or sets the filtered event type.
    /// </summary>
    [FromQuery(Name = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the filtered event subtype.
    /// </summary>
    [FromQuery(Name = "subtype")]
    public string Subtype { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    [FromQuery(Name = "startDate")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    [FromQuery(Name = "endDate")]
    public DateTime? EndDate { get; set; }
}