namespace Roblox.SystemEvents.Entities;

/// <summary>
/// Extended properties for the Event entity
/// </summary>
internal partial class Event
{
    /// <summary>
    /// Gets the <see cref="Entities.EventType"/> entity.
    /// </summary>
    public EventType EventType => EventType.Get(EventTypeID);

    /// <summary>
    /// Gets the optional <see cref="Entities.EventSubtype"/> entity.
    /// </summary>
    public EventSubtype EventSubtype => EventSubtypeID != null ? EventSubtype.Get(EventSubtypeID.Value) : null;

    /// <summary>
    /// Gets the <see cref="Entities.EventSummary"/> entity.
    /// </summary>
    public EventSummary EventSummary => EventSummary.Get(EventSummaryID);
}