namespace Roblox.SystemEvents;

using System;

/// <summary>
/// Represents an event model.
/// </summary>
public class EventModel
{
    /// <summary>
    /// Gets or sets the ID of the event.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the event type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the event subtype.
    /// </summary>
    public string Subtype { get; set; }

    /// <summary>
    /// Gets or sets the event summary.
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets the event timestamp.
    /// </summary>
    public DateTime Timestamp { get; set; }
}