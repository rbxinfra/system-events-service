namespace Roblox.SystemEvents.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

internal class EventSubtypeDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxSystemEvents;

    public int ID { get; set; }
    public string Value { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    private static EventSubtypeDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new EventSubtypeDAL();
        dal.ID = (int)record["ID"];
        dal.Value = (string)record["Value"];
        dal.Created = (DateTime)record["Created"];
        dal.Updated = (DateTime)record["Updated"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("EventSubtypes_DeleteEventSubtypeByID", ID);
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

        ID = _Database.Insert<int>("EventSubtypes_InsertEventSubtype", queryParameters);
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

        _Database.Update("EventSubtypes_UpdateEventSubtypeByID", queryParameters);
    }

    internal static EventSubtypeDAL Get(int id)
    {
        return _Database.Get(
            "EventSubtypes_GetEventSubtypeByID",
            id,
            BuildDAL
        );
    }

    public static EventSubtypeDAL GetByValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@Value", value),
        };

        return _Database.Lookup(
            "EventSubtypes_GetEventSubtypeByValue",
            BuildDAL,
            queryParameters
        );
    }

    public static ICollection<EventSubtypeDAL> MultiGet(IEnumerable<int> ids)
    {
        return _Database.MultiGet(
            "EventSubtypes_GetEventSubtypesByIDs",
            ids,
            BuildDAL
        );
    }

    public static EntityHelper.GetOrCreateDALWrapper<EventSubtypeDAL> GetOrCreateEventSubtype(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@CreatedNewEntity", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@Value", value),
        };

        return _Database.GetOrCreate(
            "EventSubtypes_GetOrCreateEventSubtype",
            BuildDAL,
            queryParameters
        );
    }
}