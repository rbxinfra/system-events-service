namespace Roblox.SystemEvents.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

internal class EventSummaryDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxSystemEvents;

    public long ID { get; set; }
    public string Hash { get; set; }
    public string Value { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    private static EventSummaryDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new EventSummaryDAL();
        dal.ID = (long)record["ID"];
        dal.Hash = (string)record["Hash"];
        dal.Value = (string)record["Value"];
        dal.Created = (DateTime)record["Created"];
        dal.Updated = (DateTime)record["Updated"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("EventSummaries_DeleteEventSummaryByID", ID);
    }

    internal void Insert()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID) { Direction = ParameterDirection.Output },
            new SqlParameter("@Hash", Hash),
            new SqlParameter("@Value", Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated),
        };

        ID = _Database.Insert<long>("EventSummaries_InsertEventSummary", queryParameters);
    }

    internal void Update()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@Hash", Hash),
            new SqlParameter("@Value", Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated),
        };

        _Database.Update("EventSummaries_UpdateEventSummaryByID", queryParameters);
    }

    internal static EventSummaryDAL Get(long id)
    {
        return _Database.Get(
            "EventSummaries_GetEventSummaryByID",
            id,
            BuildDAL
        );
    }

    public static EventSummaryDAL GetByHash(string hash)
    {
        if (string.IsNullOrEmpty(hash))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@Hash", hash),
        };

        return _Database.Lookup(
            "EventSummaries_GetEventSummaryByHash",
            BuildDAL,
            queryParameters
        );
    }

    public static ICollection<EventSummaryDAL> MultiGet(IEnumerable<long> ids)
    {
        return _Database.MultiGet(
            "EventSummaries_GetEventSummarysByIDs",
            ids,
            BuildDAL
        );
    }

    public static EntityHelper.GetOrCreateDALWrapper<EventSummaryDAL> GetOrCreateEventSummaryByHashAndValue(string hash, string value)
    {
        if (string.IsNullOrEmpty(hash))
            return null;
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@CreatedNewEntity", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@Hash", hash),
            new SqlParameter("@Value", value),
        };

        return _Database.GetOrCreate(
            "EventSummaries_GetOrCreateEventSummaryByHashAndValue",
            BuildDAL,
            queryParameters
        );
    }
}