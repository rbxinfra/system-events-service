namespace Roblox.SystemEvents;

using Newtonsoft.Json;

/// <summary>
/// Model for the output of <see cref="QueryEventsOperation"/>
/// </summary>
public class QueryEventsPayload
{
    /// <summary>
    /// Sets the count of items.
    /// </summary>
    [JsonProperty("data")]
    public PagedResultModel<EventModel> Data { get; set; }
}