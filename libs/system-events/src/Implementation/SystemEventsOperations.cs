namespace Roblox.SystemEvents;

using System;

using EventLog;

/// <summary>
/// Default implementation for <see cref="ISystemEventsOperations"/>
/// </summary>
public class SystemEventsOperations : ISystemEventsOperations
{
    /// <inheritdoc cref="ISystemEventsOperations.LogEventOperation"/>
    public LogEventOperation LogEventOperation { get; }

    /// <inheritdoc cref="ISystemEventsOperations.QueryEventsOperation"/>
    public QueryEventsOperation QueryEventsOperation { get; }

    /// <summary>
    /// Construct a new instance of <see cref="SystemEventsOperations"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> for debug logging.</param>
    /// <param name="settings">The <see cref="ISettings"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="settings"/> cannot be null.
    /// </exception>
    public SystemEventsOperations(ILogger logger, ISettings settings)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        LogEventOperation = new(logger, settings);
        QueryEventsOperation = new(logger);
    }
}