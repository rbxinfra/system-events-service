namespace Roblox.SystemEvents;

using Caching;
using Configuration;

using static SettingsProvidersDefaults;

/// <summary>
/// The settings for the application.
/// </summary>
internal class Settings : BaseSettingsProvider<Settings>, ISettings
{
    private static readonly MigrationStateChange _NoMigration = new("0,0,0");


    /// <inheritdoc cref="BaseSettingsProvider.ChildPath"/>
    protected override string ChildPath => SystemEventsSettingsPath;

    /// <inheritdoc cref="ISettings.MaximumEventSummaryLength"/>
    public int MaximumEventSummaryLength => GetOrDefault(nameof(MaximumEventSummaryLength), 10000);

    /// <inheritdoc cref="ISettings.MemcachedGroupName"/>
    public string MemcachedGroupName => GetOrDefault(nameof(MemcachedGroupName), "SystemEvents");

    /// <inheritdoc cref="ISettings.MigrationMemcachedGroupName"/>
    public string MigrationMemcachedGroupName => GetOrDefault(nameof(MigrationMemcachedGroupName), "SystemEventsMcRouter");

    /// <inheritdoc cref="ISettings.EventEntityMigrationStage"/>
    public MigrationStateChange EventEntityMigrationStage => GetOrDefault(nameof(EventEntityMigrationStage), _NoMigration);

    /// <inheritdoc cref="ISettings.EventTypeEntityMigrationStage"/>
    public MigrationStateChange EventTypeEntityMigrationStage => GetOrDefault(nameof(EventTypeEntityMigrationStage), _NoMigration);

    /// <inheritdoc cref="ISettings.EventSubtypeEntityMigrationStage"/>
    public MigrationStateChange EventSubtypeEntityMigrationStage => GetOrDefault(nameof(EventSubtypeEntityMigrationStage), _NoMigration);

    /// <inheritdoc cref="ISettings.EventSummaryEntityMigrationStage"/>
    public MigrationStateChange EventSummaryEntityMigrationStage => GetOrDefault(nameof(EventSummaryEntityMigrationStage), _NoMigration);
    
}