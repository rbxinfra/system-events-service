namespace Roblox.SystemEvents.Service;

using System;

using Microsoft.Extensions.DependencyInjection;

using Web.Framework.Services;
using Web.Framework.Services.Http;

using EventLog;
using Configuration;

using SystemEventsSettings = Roblox.SystemEvents.Settings;
using ISystemEventsSettings = Roblox.SystemEvents.ISettings;

using SystemEventsServiceSettings = Roblox.SystemEvents.Service.Settings;

/// <summary>
/// Startup class for system events service.
/// </summary>
public class Startup : HttpStartupBase
{
    /// <inheritdoc cref="StartupBase.Settings"/>
    protected override IServiceSettings Settings => SystemEventsServiceSettings.Singleton;

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
        services.AddSingleton<ISystemEventsOperations, SystemEventsOperations>();
    }
}