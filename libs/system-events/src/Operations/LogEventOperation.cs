namespace Roblox.SystemEvents;

using System;

using EventLog;
using Operations;

using Entities;
using Roblox.Hashing;

using EventEntity = Entities.Event;

/// <summary>
/// Operation used to log an event.
/// </summary>
/// <param name="logger">The <see cref="ILogger"/> for debug logging.</param>
/// <param name="settings">The <see cref="ISettings"/>.</param>
public class LogEventOperation(ILogger logger, ISettings settings) : IResultOperation<LogEventInput, EventModel>
{
    private readonly ILogger _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ISettings _Settings = settings ?? throw new ArgumentNullException(nameof(settings));

    /// <inheritdoc cref="IResultOperation{TRequest, TResult}.Execute(TRequest)"/>
    public (EventModel Output, OperationError Error) Execute(LogEventInput input)
    {
        if (string.IsNullOrEmpty(input.Type)) return (null, new(SystemEventsErrors.TypeNotSpecified));
        if (string.IsNullOrEmpty(input.Summary)) return (null, new(SystemEventsErrors.SummaryNotSpecified));
        if (input.Summary.Length > _Settings.MaximumEventSummaryLength) return (null, new(SystemEventsErrors.SummaryLengthTooLong, _Settings.MaximumEventSummaryLength));

        _Logger.Information("Logging new system event, Type = '{0}', Subtype = '{1}', Summary = '{2}'", input.Type, input.Subtype ?? "(none)", input.Summary);

        var eventType = EventType.GetOrCreate(input.Type);
        EventSubtype eventSubtype = !string.IsNullOrEmpty(input.Subtype) ? EventSubtype.GetOrCreate(input.Subtype) : null;

        var summaryHash = SHA256Hasher.BuildSHA256HashString(input.Summary);
        var eventSummary = EventSummary.GetOrCreate(summaryHash, input.Summary);

        var @event = EventEntity.CreateNew(eventType.ID, eventSubtype?.ID, eventSummary.ID);

        return (new()
        {
            Id = @event.ID,
            Type = eventType.Value,
            Subtype = eventSubtype?.Value ?? "",
            Summary = eventSummary.Value,
            Timestamp = @event.Created
        }, null);
    }
}