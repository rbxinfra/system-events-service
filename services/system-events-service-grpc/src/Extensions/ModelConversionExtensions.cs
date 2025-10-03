namespace Roblox.SystemEvents.Service;

using System;
using System.Linq;

using Google.Protobuf.WellKnownTypes;

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
    /// Converts a <see cref="LogEventPayload"/> to a <see cref="LogEventResponse"/>
    /// </summary>
    /// <param name="response">The <see cref="LogEventPayload"/></param>
    /// <returns>The <see cref="LogEventResponse"/></returns>
    public static LogEventResponse ToGrpc(this LogEventPayload response)
        => new() { Event = response.Data.ToGrpc() };

    /// <summary>
    /// Converts a <see cref="QueryEventsPayload"/> to a <see cref="QueryEventsResponse"/>
    /// </summary>
    /// <param name="response">The <see cref="QueryEventsPayload"/></param>
    /// <returns>The <see cref="QueryEventsResponse"/></returns>
    public static QueryEventsResponse ToGrpc(this QueryEventsPayload response)
    {
        var grpcResponse = new QueryEventsResponse
        {
            Count = response.Data.Count
        };

        grpcResponse.PageItems.AddRange(response.Data.PageItems.Select(item => item.ToGrpc()));

        return grpcResponse;
    }
}