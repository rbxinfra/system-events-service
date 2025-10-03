namespace Roblox.SystemEvents;

using Newtonsoft.Json;

/// <summary>
/// Model for the output of <see cref="LogEventOperation"/>
/// </summary>
public class LogEventPayload
{
    /// <summary>
    /// Gets or sets the logged event.
    /// </summary>
    [JsonProperty("data")]
    public EventModel Data { get; set; }
}