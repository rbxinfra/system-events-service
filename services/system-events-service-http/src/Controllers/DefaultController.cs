namespace Roblox.SystemEvents.Service.Controllers;

using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Web.Framework.Services.Http;

/// <summary>
/// Health check controller.
/// </summary>
[Route("")]
[ApiController]
#if DEBUG
[AllowAnonymous]
#endif
public class DefaultController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly ISystemEventsOperations _SystemEventsOperations;

    /// <summary>
    /// Construct a new instance of <see cref="DefaultController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="systemEventsOperations">The <see cref="ISystemEventsOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="systemEventsOperations"/> cannot be null.
    /// </exception>
    public DefaultController(IOperationExecutor operationExecutor, ISystemEventsOperations systemEventsOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _SystemEventsOperations = systemEventsOperations ?? throw new ArgumentNullException(nameof(systemEventsOperations));
    }

    /// <summary>
    /// Logs a new event.
    /// </summary>
    /// <param name="request">The <see cref="LogEventInput"/></param>
    /// <returns>The newly logged event.</returns>
    /// <response code="400">
    /// <li>The type was not specifed in the request</li>
    /// <li>The summary was not specifed in the request</li>
    /// <li>Summary must not exceed N characters in length!</li>
    /// </response>
    [HttpPost($"/v1/{nameof(LogEvent)}")]
    [ProducesResponseType(200, Type = typeof(LogEventPayload))]
    [ProducesResponseType(400)]
    public IActionResult LogEvent([FromBody][ValidateNever] LogEventInput request)
        => _OperationExecutor.Execute(_SystemEventsOperations.LogEventOperation, request);

    /// <summary>
    /// Gets a list of events by a specific query.
    /// </summary>
    /// <param name="request">The <see cref="QueryEventsInput"/></param>
    /// <returns>A paged list of entities.</returns>
    /// <response code="400">
    /// <li>Start date must be specified alongside end date.</li>
    /// </response>
    [HttpGet($"/v1/{nameof(QueryEvents)}")]
    [ProducesResponseType(200, Type = typeof(QueryEventsPayload))]
    [ProducesResponseType(400)]
    public IActionResult QueryEvents([FromQuery][ValidateNever] QueryEventsInput request)
        => _OperationExecutor.Execute(_SystemEventsOperations.QueryEventsOperation, request);
}