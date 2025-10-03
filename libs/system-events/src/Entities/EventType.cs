namespace Roblox.SystemEvents.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

internal class EventType : IRobloxEntity<int, EventTypeDAL>, IRemoteCacheableObject
{
    private EventTypeDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public int ID
    {
        get { return _EntityDAL.ID; }
    }

    public string Value
    {
        get { return _EntityDAL.Value; }
        set { _EntityDAL.Value = value; }
    }

    public DateTime Created
    {
        get { return _EntityDAL.Created; }
        set { _EntityDAL.Created = value; }
    }

    public DateTime Updated
    {
        get { return _EntityDAL.Updated; }
        set { _EntityDAL.Updated = value; }
    }

    public EventType()
    { 
        _EntityDAL = new EventTypeDAL();
    }

    internal EventType(EventTypeDAL dal)
    {
        _EntityDAL = dal;
    }


    internal void Delete()
    {
        EntityHelper.DeleteEntityWithRemoteCache(
            this,
            _EntityDAL.Delete
        );
    }

    internal void Save()
    {
        EntityHelper.SaveEntityWithRemoteCache(
            this, 
            () =>
            {
                _EntityDAL.Created = DateTime.Now;
                _EntityDAL.Updated = _EntityDAL.Created;
                _EntityDAL.Insert();
            }, 
            () =>
            {
                _EntityDAL.Updated = DateTime.Now;
                _EntityDAL.Update();
            }
        );
    }

    internal static EventType Get(int id)
    {
        return EntityHelper.GetEntity<int, EventTypeDAL, EventType>(
            EntityCacheInfo, 
            id, 
            () => EventTypeDAL.Get(id)
        );
    }

    public static EventType GetByValue(string value)
    {
        return EntityHelper.GetEntityByLookupWithRemoteCache<int, EventTypeDAL, EventType>(
            EntityCacheInfo,
            string.Format("Value:{0}", value),
            () => EventTypeDAL.GetByValue(value),
            Get
        );
    }

    public static ICollection<EventType> MultiGet(IEnumerable<int> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (!ids.Any()) return Array.Empty<EventType>();

        return EntityHelper.GetEntitiesByIds<EventType, EventTypeDAL, int>(
            EntityCacheInfo,
            ids.Distinct().ToList(),
            EventTypeDAL.MultiGet
        ).ToList();
    }

    public static int GetTotalNumberOfEventTypes()
    {
        var countId = "GetTotalNumberOfEventTypes";

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            countId,
            () => EventTypeDAL.GetTotalNumberOfEventTypes()
        );
    }

    public static EventType GetOrCreate(string value)
    {
        return EntityHelper.GetOrCreateEntityWithRemoteCache<int, EventType>(
            EntityCacheInfo,
            string.Format("Value:{0}", value),
            () => DoGetOrCreate(value),
            Get
        );
    }

    private static EventType DoGetOrCreate(string value)
    {
        return EntityHelper.DoGetOrCreate<int, EventTypeDAL, EventType>(
            () => EventTypeDAL.GetOrCreateEventType(value)
        );
    }

    public static ICollection<EventType> GetAllPaged(long startRowIndex, long maximumRows)
    {
        var collectionId = "GetAllPaged";

        return EntityHelper.GetEntityCollection<EventType, int>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            collectionId,
            () =>
            {
                return EventTypeDAL.GetAllPaged(
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(EventTypeDAL dal)
    {
        _EntityDAL = dal;
    }

    #endregion IRobloxEntity Members

    #region ICacheableObject Members

    /// <inheritdoc cref="ICacheableObject.CacheInfo"/>
    public CacheInfo CacheInfo
    {
        get { return EntityCacheInfo; }
    }

    /// <inheritdoc cref="ICacheableObject.CacheInfo"/>
    public static CacheInfo EntityCacheInfo = new CacheInfo(
        new CacheabilitySettings(collectionsAreCacheable: false, countsAreCacheable: false, entityIsCacheable: true, idLookupsAreCacheable: true, hasUnqualifiedCollections: false, idLookupsAreCaseSensitive: false),
        typeof(EventType).ToString(),
        true,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.RemoteCacheableSettings,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.EventTypeEntityMigrationCacheableSettings
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        yield return string.Format("Value:{0}", Value);
        yield break;
    }

    /// <inheritdoc cref="ICacheableObject.BuildStateTokenCollection"/>
    public IEnumerable<StateToken> BuildStateTokenCollection()
    {
        yield break;
    }

    #endregion ICacheableObject Members

    #region IRemoteCacheableObject Members

    /// <inheritdoc cref="IRemoteCacheableObject.GetSerializable"/>
    public object GetSerializable()
    {
        return _EntityDAL;
    }

    #endregion IRemoteCacheableObject Members
}