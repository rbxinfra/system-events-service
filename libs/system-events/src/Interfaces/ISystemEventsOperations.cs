namespace Roblox.SystemEvents;

/// <summary>
/// Operations accessor for system-events-service.
/// </summary>
public interface ISystemEventsOperations
{
    /// <summary>
    /// Gets the log event operation.
    /// </summary>
    LogEventOperation LogEventOperation { get; }

    /// <summary>
    /// Gets the query events operation.
    /// </summary>
    QueryEventsOperation QueryEventsOperation { get; }
}