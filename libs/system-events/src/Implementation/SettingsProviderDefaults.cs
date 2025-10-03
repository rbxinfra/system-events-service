namespace Roblox.SystemEvents;

/// <summary>
/// Default details for the settings providers.
/// </summary>
internal static class SettingsProvidersDefaults
{
    /// <summary>
    /// The path prefix for the web platform.
    /// </summary>
    public const string ProviderPathPrefix = "services";

    /// <summary>
    /// The path to the system-events settings.
    /// </summary>
    public const string SystemEventsSettingsPath = $"{ProviderPathPrefix}/system-events";

    /// <summary>
    /// The path to the system-events service settings.
    /// </summary>
    public const string SystemEventsServiceSettingsPath = $"{ProviderPathPrefix}/system-events-service";
}