namespace Roblox.SystemEvents.Service;

using EventLog;
using Configuration;

using Web.Framework.Services;

using static SettingsProvidersDefaults;

/// <summary>
/// Settings for system events service
/// </summary>
internal class Settings : BaseSettingsProvider<Settings>, IServiceSettings
{
    /// <inheritdoc cref="IVaultProvider.Path"/>
    protected override string ChildPath => SystemEventsServiceSettingsPath;

    /// <inheritdoc cref="IServiceSettings.ApiKey"/>
    public string ApiKey => GetOrDefault(nameof(ApiKey), string.Empty);

    /// <inheritdoc cref="IServiceSettings.LogLevel"/>
    public LogLevel LogLevel => GetOrDefault(nameof(LogLevel), LogLevel.Information);

    /// <inheritdoc cref="IServiceSettings.VerboseErrorsEnabled"/>
    public bool VerboseErrorsEnabled => GetOrDefault(nameof(VerboseErrorsEnabled), false);
}