namespace Roblox.SystemEvents.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

internal partial class Event : IRobloxEntity<long, EventDAL>, IRemoteCacheableObject
{
    private EventDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    public int EventTypeID
    {
        get { return _EntityDAL.EventTypeID; }
        set { _EntityDAL.EventTypeID = value; }
    }

    public int? EventSubtypeID
    {
        get { return _EntityDAL.EventSubtypeID; }
        set { _EntityDAL.EventSubtypeID = value; }
    }

    public long EventSummaryID
    {
        get { return _EntityDAL.EventSummaryID; }
        set { _EntityDAL.EventSummaryID = value; }
    }

    public DateTime Created
    {
        get { return _EntityDAL.Created; }
        set { _EntityDAL.Created = value; }
    }

    public Event()
    { 
        _EntityDAL = new EventDAL();
    }

    internal Event(EventDAL dal)
    {
        _EntityDAL = dal;
    }

    internal static Event CreateNew(int eventTypeID, int? eventSubtypeID, long eventSummaryID)
    {
        var entity = new Event();
        entity.EventTypeID = eventTypeID;
        entity.EventSubtypeID = eventSubtypeID;
        entity.EventSummaryID = eventSummaryID;

        entity.Save();

        return entity;
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
                _EntityDAL.Insert();
            }, 
            _EntityDAL.Update
        );
    }

    internal static Event Get(long id)
    {
        return EntityHelper.GetEntity<long, EventDAL, Event>(
            EntityCacheInfo, 
            id, 
            () => EventDAL.Get(id)
        );
    }

    public static ICollection<Event> GetAllPaged(long startRowIndex, long maximumRows)
    {
        var collectionId = "GetAllPaged";

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            collectionId,
            () =>
            {
                return EventDAL.GetAllPaged(
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEvents()
    {
        var countId = "GetTotalNumberOfEvents";

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            countId,
            () => EventDAL.GetTotalNumberOfEvents()
        );
    }

    public static ICollection<Event> GetAllBetweenDatesPaged(DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetAllBetweenDatesPaged_StartDate:{0}_EndDate:{1}_StartRowIndex:{2}_MaximumRows:{3}", startDate, endDate, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            collectionId,
            () =>
            {
                return EventDAL.GetAllBetweenDatesPaged(
                    startDate,
                    endDate,
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEventsBetweenDates(DateTime startDate, DateTime endDate)
    {
        var countId = string.Format("GetTotalNumberOfEventsBetweenDates_StartDate:{0}_EndDate:{1}", startDate, endDate);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            CacheManager.UnqualifiedNonExpiringCachePolicy,
            countId,
            () => EventDAL.GetTotalNumberOfEventsBetweenDates(startDate, endDate)
        );
    }

    public static ICollection<Event> GetEventsByEventSubtypeIDPaged(int eventSubtypeID, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetEventsByEventSubtypeIDPaged_EventSubtypeID:{0}_StartRowIndex:{1}_MaximumRows:{2}", eventSubtypeID, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventSubtypeID:{0}", eventSubtypeID)
            ),
            collectionId,
            () =>
            {
                return EventDAL.GetEventsByEventSubtypeIDPaged(
                    eventSubtypeID,
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEventsByEventSubtypeID(int eventSubtypeID)
    {
        var countId = string.Format("GetTotalNumberOfEventsByEventSubtypeID_EventSubtypeID:{0}", eventSubtypeID);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventSubtypeID:{0}", eventSubtypeID)
            ),
            countId,
            () => EventDAL.GetTotalNumberOfEventsByEventSubtypeID(eventSubtypeID)
        );
    }

    public static ICollection<Event> GetEventsByEventSubtypeIDBetweenDatesPaged(int eventSubtypeID, DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetEventsByEventSubtypeIDBetweenDatesPaged_EventSubtypeID:{0}_StartDate:{1}_EndDate:{2}_StartRowIndex:{3}_MaximumRows:{4}", eventSubtypeID, startDate, endDate, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventSubtypeID:{0}", eventSubtypeID)
            ),
            collectionId,
            () =>
            {
                return EventDAL.GetEventsByEventSubtypeIDBetweenDatesPaged(
                    eventSubtypeID,
                    startDate,
                    endDate,
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEventsByEventSubtypeIDBetweenDates(int eventSubtypeID, DateTime startDate, DateTime endDate)
    {
        var countId = string.Format("GetTotalNumberOfEventsByEventSubtypeIDBetweenDates_EventSubtypeID:{0}_StartDate:{1}_EndDate:{2}", eventSubtypeID, startDate, endDate);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventSubtypeID:{0}", eventSubtypeID)
            ),
            countId,
            () => EventDAL.GetTotalNumberOfEventsByEventSubtypeIDBetweenDates(eventSubtypeID, startDate, endDate)
        );
    }

    public static ICollection<Event> GetEventsByEventTypeIDPaged(int eventTypeID, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetEventsByEventTypeIDPaged_EventTypeID:{0}_StartRowIndex:{1}_MaximumRows:{2}", eventTypeID, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventTypeID:{0}", eventTypeID)
            ),
            collectionId,
            () =>
            {
                return EventDAL.GetEventsByEventTypeIDPaged(
                    eventTypeID,
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEventsByEventTypeID(int eventTypeID)
    {
        var countId = string.Format("GetTotalNumberOfEventsByEventTypeID_EventTypeID:{0}", eventTypeID);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventTypeID:{0}", eventTypeID)
            ),
            countId,
            () => EventDAL.GetTotalNumberOfEventsByEventTypeID(eventTypeID)
        );
    }

    public static ICollection<Event> GetEventsByEventTypeIDBetweenDatesPaged(int eventTypeID, DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetEventsByEventTypeIDBetweenDatesPaged_EventTypeID:{0}_StartDate:{1}_EndDate:{2}_StartRowIndex:{3}_MaximumRows:{4}", eventTypeID, startDate, endDate, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<Event, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventTypeID:{0}", eventTypeID)
            ),
            collectionId,
            () =>
            {
                return EventDAL.GetEventsByEventTypeIDBetweenDatesPaged(
                    eventTypeID,
                    startDate,
                    endDate,
                    startRowIndex,
                    maximumRows
                );
            },
            Get
        );
    }

    public static int GetTotalNumberOfEventsByEventTypeIDBetweenDates(int eventTypeID, DateTime startDate, DateTime endDate)
    {
        var countId = string.Format("GetTotalNumberOfEventsByEventTypeIDBetweenDates_EventTypeID:{0}_StartDate:{1}_EndDate:{2}", eventTypeID, startDate, endDate);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("EventTypeID:{0}", eventTypeID)
            ),
            countId,
            () => EventDAL.GetTotalNumberOfEventsByEventTypeIDBetweenDates(eventTypeID, startDate, endDate)
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(EventDAL dal)
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
        typeof(Event).ToString(),
        true,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.RemoteCacheableSettings,
        global::Roblox.SystemEvents.SystemEventsMigrationSettings.EventEntityMigrationCacheableSettings
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        yield break;
    }

    /// <inheritdoc cref="ICacheableObject.BuildStateTokenCollection"/>
    public IEnumerable<StateToken> BuildStateTokenCollection()
    {
        yield return new StateToken();
        yield return new StateToken(string.Format("EventSubtypeID:{0}", EventSubtypeID));
        yield return new StateToken(string.Format("EventSubtypeID:{0}", EventSubtypeID));
        yield return new StateToken(string.Format("EventTypeID:{0}", EventTypeID));
        yield return new StateToken(string.Format("EventTypeID:{0}", EventTypeID));
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