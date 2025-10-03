namespace Roblox.SystemEvents.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

internal class EventTypeDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxSystemEvents;

    public int ID { get; set; }
    public string Value { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    private static EventTypeDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new EventTypeDAL();
        dal.ID = (int)record["ID"];
        dal.Value = (string)record["Value"];
        dal.Created = (DateTime)record["Created"];
        dal.Updated = (DateTime)record["Updated"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("EventTypes_DeleteEventTypeByID", ID);
    }

    internal void Insert()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID) { Direction = ParameterDirection.Output },
            new SqlParameter("@Value", Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated),
        };

        ID = _Database.Insert<int>("EventTypes_InsertEventType", queryParameters);
    }

    internal void Update()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@Value", Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated),
        };

        _Database.Update("EventTypes_UpdateEventTypeByID", queryParameters);
    }

    internal static EventTypeDAL Get(int id)
    {
        return _Database.Get(
            "EventTypes_GetEventTypeByID",
            id,
            BuildDAL
        );
    }

    public static EventTypeDAL GetByValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@Value", value),
        };

        return _Database.Lookup(
            "EventTypes_GetEventTypeByValue",
            BuildDAL,
            queryParameters
        );
    }

    public static ICollection<EventTypeDAL> MultiGet(IEnumerable<int> ids)
    {
        return _Database.MultiGet(
            "EventTypes_GetEventTypesByIDs",
            ids,
            BuildDAL
        );
    }

    public static int GetTotalNumberOfEventTypes()
    {
        return _Database.GetCount<int>(
            "EventTypes_GetTotalNumberOfEventTypes"
        );
    }

    public static EntityHelper.GetOrCreateDALWrapper<EventTypeDAL> GetOrCreateEventType(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@CreatedNewEntity", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@Value", value),
        };

        return _Database.GetOrCreate(
            "EventTypes_GetOrCreateEventType",
            BuildDAL,
            queryParameters
        );
    }

    public static ICollection<int> GetAllPaged(long startRowIndex, long maximumRows)
    {
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<int>(
            "EventTypes_GetAllEventTypeIDs_Paged",
            queryParameters
        );
    }
}