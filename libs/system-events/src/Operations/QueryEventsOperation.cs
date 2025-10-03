namespace Roblox.SystemEvents;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;

using Entities;

using EventEntity = Entities.Event;

/// <summary>
/// Operation used to query system events.
/// </summary>
/// <param name="logger">The <see cref="ILogger"/> used for debug logging</param>
public class QueryEventsOperation(ILogger logger) : IResultOperation<QueryEventsInput, QueryEventsPayload>
{
    private readonly ILogger _Logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (QueryEventsPayload Output, OperationError Error) Execute(QueryEventsInput input)
    {
        var page = input.Page;
        var pageSize = input.PageSize;

        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 50;

        var startRowIndex = pageSize * (page - 1);

        DateTime? startDate = input.StartDate;
        DateTime? endDate = input.EndDate;

        if (startDate != null)
            if (endDate == null) endDate = DateTime.Now;

        if (endDate != null)
            if (startDate == null) return (null, new(SystemEventsErrors.StartDateMustBeSpecifiedWithEndDate));

        _Logger.Information(
            "Query Events, StartRowIndex = '{0}', Page = '{1}', PageSize = '{2}', StartDate = '{3}', EndDate = '{4}', Type = '{5}', Subtype = '{6}'",
            startRowIndex,
            page,
            pageSize,
            startDate?.ToString() ?? "(none)",
            endDate?.ToString() ?? "(none)",
            input.Type ?? "(none)",
            input.Subtype ?? "(none)"
        );

        int totalNumberOfEvents = 0;
        ICollection<EventEntity> entities;

        if (!string.IsNullOrEmpty(input.Type))
        {
            var eventType = EventType.GetOrCreate(input.Type);

            if (startDate != null)
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEventsByEventTypeIDBetweenDates(eventType.ID, startDate.Value, endDate.Value);
                entities = EventEntity.GetEventsByEventTypeIDBetweenDatesPaged(eventType.ID, startDate.Value, endDate.Value, startRowIndex, pageSize);
            }
            else
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEventsByEventTypeID(eventType.ID);
                entities = EventEntity.GetEventsByEventTypeIDPaged(eventType.ID, startRowIndex, pageSize);
            }
        }
        else if (!string.IsNullOrEmpty(input.Subtype))
        {
            var eventSubtype = EventSubtype.GetOrCreate(input.Subtype);

            if (startDate != null)
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEventsByEventSubtypeIDBetweenDates(eventSubtype.ID, startDate.Value, endDate.Value);
                entities = EventEntity.GetEventsByEventSubtypeIDBetweenDatesPaged(eventSubtype.ID, startDate.Value, endDate.Value, startRowIndex, pageSize);
            }
            else
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEventsByEventSubtypeID(eventSubtype.ID);
                entities = EventEntity.GetEventsByEventSubtypeIDPaged(eventSubtype.ID, startRowIndex, pageSize);
            }
        }
        else
        {
            if (startDate != null)
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEventsBetweenDates(startDate.Value, endDate.Value);
                entities = EventEntity.GetAllBetweenDatesPaged(startDate.Value, endDate.Value, startRowIndex, pageSize);
            }
            else
            {
                totalNumberOfEvents = EventEntity.GetTotalNumberOfEvents();
                entities = EventEntity.GetAllPaged(startRowIndex, pageSize);
            }
        }

        var eventTypes = entities.Select(entity => new EventModel
        {
            Id = entity.ID,
            Type = entity.EventType.Value,
            Subtype = entity.EventSubtype?.Value ?? "",
            Summary = entity.EventSummary.Value,
            Timestamp = entity.Created
        });

        var response = new PagedResultModel<EventModel>
        {
            Count = totalNumberOfEvents
        };

        response.PageItems = [.. eventTypes];

        return (new() { Data = response }, null);
    }
}