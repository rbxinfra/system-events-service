namespace Roblox.SystemEvents.Service;


using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Web.Framework.Services;
using Web.Framework.Services.Grpc;

using Redis;
using EventLog;
using Configuration;
using ServiceDiscovery;

using SystemEventsSettings = Roblox.SystemEvents.Settings;
using ISystemEventsSettings = Roblox.SystemEvents.ISettings;

using SystemEventsServiceSettings = Roblox.SystemEvents.Service.Settings;

/// <summary>
/// Startup class for jobs-state-datastore.
/// </summary>
public class Startup : GrpcStartupBase
{
    /// <inheritdoc cref="GrpcStartupBase.Settings"/>
    protected override IGrpcServiceSettings Settings => SystemEventsServiceSettings.Singleton;

    /// <inheritdoc cref="GrpcStartupBase.ConfigureEndpoints(IEndpointRouteBuilder)"/>
    protected override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        base.ConfigureEndpoints(endpoints);

        endpoints.MapGrpcService<SystemEventsAPI>();
    }

    /// <inheritdoc cref="StartupBase.ConfigureLogger(IServiceProvider)"/>
    protected override ILogger ConfigureLogger(IServiceProvider provider)
    {
        var logger = base.ConfigureLogger(provider);

        logger.CustomLogPrefixes.Add(() => RobloxEnvironment.Name);

        return logger;
    }

    /// <inheritdoc cref="StartupBase.ConfigureServices(IServiceCollection)"/>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSingleton<ISystemEventsSettings>(SystemEventsSettings.Singleton);
        services.AddSingleton<IOperationExecutor, OperationExecutor>();
        services.AddSingleton<ISystemEventsOperations, SystemEventsOperations>();
    }
}