namespace Roblox.SystemEvents;

using Caching;
using Configuration;

/// <summary>
/// Provides access to <see cref="IRemoteCachabilitySettings" /> and <see cref="IMigrationCacheabilitySettings" /> for system events.
/// </summary>
internal static class SystemEventsMigrationSettings
{
    /// <summary>
    /// The <see cref="IRemoteCachabilitySettings" /> for system events.
    /// </summary>
    public static IRemoteCachabilitySettings RemoteCacheableSettings = new RemoteCachabilitySettings(
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.MemcachedGroupName)
    );

    /// <summary>
    /// The <see cref="IMigrationCacheabilitySettings" /> for events.
    /// </summary>
    public static IMigrationCacheabilitySettings EventEntityMigrationCacheableSettings = new MigrationCacheabilitySettings(
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.MigrationMemcachedGroupName),
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.EventEntityMigrationStage)
    );

    /// <summary>
    /// The <see cref="IMigrationCacheabilitySettings" /> for event types.
    /// </summary>
    public static IMigrationCacheabilitySettings EventTypeEntityMigrationCacheableSettings = new MigrationCacheabilitySettings(
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.MigrationMemcachedGroupName),
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.EventTypeEntityMigrationStage)
    );

    /// <summary>
    /// The <see cref="IMigrationCacheabilitySettings" /> for event subtypes.
    /// </summary>
    public static IMigrationCacheabilitySettings EventSubtypeEntityMigrationCacheableSettings = new MigrationCacheabilitySettings(
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.MigrationMemcachedGroupName),
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.EventSubtypeEntityMigrationStage)
    );

    /// <summary>
    /// The <see cref="IMigrationCacheabilitySettings" /> for event summaries.
    /// </summary>
    public static IMigrationCacheabilitySettings EventSummaryEntityMigrationCacheableSettings = new MigrationCacheabilitySettings(
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.MigrationMemcachedGroupName),
        global::Roblox.SystemEvents.Settings.Singleton.ToSingleSetting(s => s.EventSummaryEntityMigrationStage)
    );
}