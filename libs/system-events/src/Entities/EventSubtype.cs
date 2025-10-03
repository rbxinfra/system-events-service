namespace Roblox.SystemEvents.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

internal class EventSubtype : IRobloxEntity<int, EventSubtypeDAL>, IRemoteCacheableObject
{
    private EventSubtypeDAL _EntityDAL;

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

    public EventSubtype()
    { 
        _EntityDAL = new EventSubtypeDAL();
    }

    internal EventSubtype(EventSubtypeDAL dal)
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

    internal static EventSubtype Get(int id)
    {
        return EntityHelper.GetEntity<int, EventSubtypeDAL, EventSubtype>(
            EntityCacheInfo, 
            id, 
            () => EventSubtypeDAL.Get(id)
        );
    }

    public static EventSubtype GetByValue(string value)
    {
        return EntityHelper.GetEntityByLookupWithRemoteCache<int, EventSubtypeDAL, EventSubtype>(
            EntityCacheInfo,
            string.Format("Value:{0}", value),
            () => EventSubtypeDAL.GetByValue(value),
            Get
        );
    }

    public static ICollection<EventSubtype> MultiGet(IEnumerable<int> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (!ids.Any()) return Array.Empty<EventSubtype>();

        return EntityHelper.GetEntitiesByIds<EventSubtype, EventSubtypeDAL, int>(
            EntityCacheInfo,
            ids.Distinct().ToList(),
            EventSubtypeDAL.MultiGet
        ).ToList();
    }

    public static EventSubtype GetOrCreate(string value)
    {
        return EntityHelper.GetOrCreateEntityWithRemoteCache<int, EventSubtype>(
            EntityCacheInfo,
            string.Format("Value:{0}", value),
            () => DoGetOrCreate(value),
            Get
        );
    }

    private static EventSubtype DoGetOrCreate(string value)
    {
        return EntityHelper.DoGetOrCreate<int, EventSubtypeDAL, EventSubtype>(
            () => EventSubtypeDAL.GetOrCreateEventSubtype(value)
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(EventSubtypeDAL dal)
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
        typeof(EventSubtype).ToString(),
        true,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.RemoteCacheableSettings,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.EventSubtypeEntityMigrationCacheableSettings
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