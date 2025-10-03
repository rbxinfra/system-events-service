namespace Roblox.SystemEvents;

/// <summary>
/// Model for the input to the <see cref="LogEventOperation"/>
/// </summary>
public class LogEventInput
{
    /// <summary>
    /// Gets or sets the event type.
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
}