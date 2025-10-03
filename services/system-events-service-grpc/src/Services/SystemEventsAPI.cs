using Grpc.Core;

namespace Roblox.SystemEvents.Service;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

using Web.Framework.Services.Grpc;

using Systemevents.Systemevents.V1;

using SystemEventsAPIGrpc = Roblox.Systemevents.Systemevents.V1.SystemEventsAPI;

/// <summary>
/// Default implementation for <see cref="SystemEventsAPIGrpc.SystemEventsAPIBase"/>
/// </summary>
#if DEBUG
[AllowAnonymous]
#endif
public class SystemEventsAPI : SystemEventsAPIGrpc.SystemEventsAPIBase
{
    private readonly IOperationExecutor _operationExecutor;
    private readonly ISystemEventsOperations _systemEventsOperations;

    /// <summary>
    /// Construct a new instance of <see cref="SystemEventsAPI"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="systemEventsOperations">The <see cref="ISystemEventsOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="systemEventsOperations"/> cannot be null.
    /// </exception>
    public SystemEventsAPI(IOperationExecutor operationExecutor, ISystemEventsOperations systemEventsOperations)
    {
        _operationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _systemEventsOperations = systemEventsOperations ?? throw new ArgumentNullException(nameof(systemEventsOperations));
    }

    /// <inheritdoc cref="SystemEventsAPIGrpc.SystemEventsAPIBase.LogEvent(LogEventRequest, ServerCallContext)"/>
    public override Task<LogEventResponse> LogEvent(LogEventRequest request, ServerCallContext context)
        => Task.FromResult(new LogEventResponse { Event = _operationExecutor.Execute(_systemEventsOperations.LogEventOperation, request.FromGrpc()).ToGrpc() });

    /// <inheritdoc cref="SystemEventsAPIGrpc.SystemEventsAPIBase.QueryEvents(QueryEventsRequest, ServerCallContext)"/>
    public override Task<QueryEventsResponse> QueryEvents(QueryEventsRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_systemEventsOperations.QueryEventsOperation, request.FromGrpc()).ToGrpc());
}