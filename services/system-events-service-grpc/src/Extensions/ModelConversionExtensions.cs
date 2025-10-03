namespace Roblox.SystemEvents.Service;

using System;
using System.Linq;

using Google.Protobuf.WellKnownTypes;

using Paging;
using Systemevents.Systemevents.V1;

/// <summary>
/// Extension methods from converting to and from gRPC models to operation models.
/// </summary>
public static class ModelConversionExtensions
{
    /// <summary>
    /// Converts a <see cref="LogEventRequest"/> to a <see cref="LogEventInput"/>
    /// </summary>
    /// <param name="request">The <see cref="LogEventRequest"/></param>
    /// <returns>The <see cref="LogEventInput"/></returns>
    public static LogEventInput FromGrpc(this LogEventRequest request)
        => new() { Type = request.Type, Subtype = request.Subtype, Summary = request.Summary };

    /// <summary>
    /// Converts a <see cref="QueryEventsRequest"/> to a <see cref="QueryEventsInput"/>
    /// </summary>
    /// <param name="request">The <see cref="QueryEventsRequest"/></param>
    /// <returns>The <see cref="QueryEventsInput"/></returns>
    public static QueryEventsInput FromGrpc(this QueryEventsRequest request)
        => new()
        {
            Page = request.Page,
            PageSize = (byte)Math.Clamp(request.PageSize, byte.MinValue, byte.MaxValue),
            Type = request.Type,
            Subtype = request.Subtype,
            StartDate = request.StartDate?.ToDateTime(),
            EndDate = request.EndDate?.ToDateTime()
        };

    /// <summary>
    /// Converts a <see cref="EventModel"/> to a <see cref="Event"/>
    /// </summary>
    /// <param name="model">The <see cref="EventModel"/></param>
    /// <returns>The <see cref="Event"/></returns>
    public static Event ToGrpc(this EventModel model)
        => new()
        {
            Id = model.Id,
            Type = model.Type,
            Subtype = model.Subtype,
            Summary = model.Summary,
            Timestamp = Timestamp.FromDateTime(DateTime.SpecifyKind(model.Timestamp, DateTimeKind.Utc))
        };

    /// <summary>
    /// Converts a <see cref="PagedResult{TCount, TPagedItem}"/> to a <see cref="QueryEventsResponse"/>
    /// </summary>
    /// <param name="response">The <see cref="PagedResult{TCount, TPagedItem}"/></param>
    /// <returns>The <see cref="QueryEventsResponse"/></returns>
    public static QueryEventsResponse ToGrpc(this PagedResult<int, EventModel> response)
    {
        var grpcResponse = new QueryEventsResponse
        {
            Count = response.Count
        };

        grpcResponse.PageItems.AddRange(response.PageItems.Select(item => item.ToGrpc()));

        return grpcResponse;
    }
}