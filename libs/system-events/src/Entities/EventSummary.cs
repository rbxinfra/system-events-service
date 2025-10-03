namespace Roblox.SystemEvents.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

internal class EventSummary : IRobloxEntity<long, EventSummaryDAL>, IRemoteCacheableObject
{
    private EventSummaryDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    public string Hash
    {
        get { return _EntityDAL.Hash; }
        set { _EntityDAL.Hash = value; }
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

    public EventSummary()
    { 
        _EntityDAL = new EventSummaryDAL();
    }

    internal EventSummary(EventSummaryDAL dal)
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

    internal static EventSummary Get(long id)
    {
        return EntityHelper.GetEntity<long, EventSummaryDAL, EventSummary>(
            EntityCacheInfo, 
            id, 
            () => EventSummaryDAL.Get(id)
        );
    }

    public static EventSummary GetByHash(string hash)
    {
        return EntityHelper.GetEntityByLookupWithRemoteCache<long, EventSummaryDAL, EventSummary>(
            EntityCacheInfo,
            string.Format("Hash:{0}", hash),
            () => EventSummaryDAL.GetByHash(hash),
            Get
        );
    }

    public static ICollection<EventSummary> MultiGet(IEnumerable<long> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (!ids.Any()) return Array.Empty<EventSummary>();

        return EntityHelper.GetEntitiesByIds<EventSummary, EventSummaryDAL, long>(
            EntityCacheInfo,
            ids.Distinct().ToList(),
            EventSummaryDAL.MultiGet
        ).ToList();
    }

    public static EventSummary GetOrCreate(string hash, string value)
    {
        return EntityHelper.GetOrCreateEntityWithRemoteCache<long, EventSummary>(
            EntityCacheInfo,
            string.Format("Hash:{0}", hash),
            () => DoGetOrCreate(hash, value),
            Get
        );
    }

    private static EventSummary DoGetOrCreate(string hash, string value)
    {
        return EntityHelper.DoGetOrCreate<long, EventSummaryDAL, EventSummary>(
            () => EventSummaryDAL.GetOrCreateEventSummaryByHashAndValue(hash, value)
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(EventSummaryDAL dal)
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
        typeof(EventSummary).ToString(),
        true,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.RemoteCacheableSettings,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.EventSummaryEntityMigrationCacheableSettings
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        yield return string.Format("Hash:{0}", Hash);
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
