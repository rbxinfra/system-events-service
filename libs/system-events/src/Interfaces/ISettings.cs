namespace Roblox.SystemEvents;

using Caching;

/// <summary>
/// Represents the settings for system events.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Gets the maximum event summary length.
    /// </summary>
    int MaximumEventSummaryLength { get; }

    /// <summary>
    /// Gets the group name for the memcached group.
    /// </summary>
    string MemcachedGroupName { get; }

    /// <summary>
    /// Gets the group name for the migration memcached group.
    /// </summary>
    string MigrationMemcachedGroupName { get; }

    /// <summary>
    /// Migration state for the <see cref="Entities.Event"/> entity.
    /// </summary>
    MigrationStateChange EventEntityMigrationStage { get; }

    /// <summary>
    /// Migration state for the <see cref="Entities.EventType"/> entity entity.
    /// </summary>
    MigrationStateChange EventTypeEntityMigrationStage { get; }

    /// <summary>
    /// Migration state for the <see cref="Entities.EventSubtype"/> entity entity.
    /// </summary>
    MigrationStateChange EventSubtypeEntityMigrationStage { get; }

    /// <summary>
    /// Migration state for the <see cref="Entities.EventSummary"/> entity entity.
    /// </summary>
    MigrationStateChange EventSummaryEntityMigrationStage { get; }
}