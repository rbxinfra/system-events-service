namespace Roblox.SystemEvents.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

internal class EventDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxSystemEvents;

    public long ID { get; set; }
    public int EventTypeID { get; set; }
    public int? EventSubtypeID { get; set; }
    public long EventSummaryID { get; set; }
    public DateTime Created { get; set; }

    private static EventDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new EventDAL();
        dal.ID = (long)record["ID"];
        dal.EventTypeID = (int)record["EventTypeID"];
        dal.EventSubtypeID = record["EventSubtypeID"] != null ? (int)record["EventSubtypeID"] : default(int);
        dal.EventSummaryID = (long)record["EventSummaryID"];
        dal.Created = (DateTime)record["Created"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("Events_DeleteEventByID", ID);
    }

    internal void Insert()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID) { Direction = ParameterDirection.Output },
            new SqlParameter("@EventTypeID", EventTypeID),
            new SqlParameter("@EventSubtypeID", EventSubtypeID == null ? DBNull.Value : (object)EventSubtypeID),
            new SqlParameter("@EventSummaryID", EventSummaryID),
            new SqlParameter("@Created", Created),
        };

        ID = _Database.Insert<long>("Events_InsertEvent", queryParameters);
    }

    internal void Update()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@EventTypeID", EventTypeID),
            new SqlParameter("@EventSubtypeID", EventSubtypeID == null ? DBNull.Value : (object)EventSubtypeID),
            new SqlParameter("@EventSummaryID", EventSummaryID),
            new SqlParameter("@Created", Created),
        };

        _Database.Update("Events_UpdateEventByID", queryParameters);
    }

    internal static EventDAL Get(long id)
    {
        return _Database.Get(
            "Events_GetEventByID",
            id,
            BuildDAL
        );
    }

    public static ICollection<long> GetAllPaged(long startRowIndex, long maximumRows)
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

        return _Database.GetIDCollection<long>(
            "Events_GetAllEventIDs_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEvents()
    {
        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEvents"
        );
    }

    public static ICollection<long> GetAllBetweenDatesPaged(DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        if (startDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'startDate' cannot be null, empty or the default value.");
        if (endDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'endDate' cannot be null, empty or the default value.");
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "Events_GetAllEventIDsBetweenDates_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEventsBetweenDates(DateTime startDate, DateTime endDate)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
        };

        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEventsBetweenDates",
            queryParameters: queryParameters
        );
    }

    public static ICollection<long> GetEventsByEventSubtypeIDPaged(int eventSubtypeID, long startRowIndex, long maximumRows)
    {
        if (eventSubtypeID == default(int)) 
            throw new ArgumentException("Parameter 'eventSubtypeID' cannot be null, empty or the default value.");
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventSubtypeID", eventSubtypeID),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "Events_GetEventIDsByEventSubtypeID_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEventsByEventSubtypeID(int eventSubtypeID)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventSubtypeID", eventSubtypeID),
        };

        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEventsByEventSubtypeID",
            queryParameters: queryParameters
        );
    }

    public static ICollection<long> GetEventsByEventSubtypeIDBetweenDatesPaged(int eventSubtypeID, DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        if (eventSubtypeID == default(int)) 
            throw new ArgumentException("Parameter 'eventSubtypeID' cannot be null, empty or the default value.");
        if (startDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'startDate' cannot be null, empty or the default value.");
        if (endDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'endDate' cannot be null, empty or the default value.");
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventSubtypeID", eventSubtypeID),
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "Events_GetEventIDsByEventSubtypeIDBetweenDates_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEventsByEventSubtypeIDBetweenDates(int eventSubtypeID, DateTime startDate, DateTime endDate)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventSubtypeID", eventSubtypeID),
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
        };

        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEventsBySubtypeIDBetweenDates",
            queryParameters: queryParameters
        );
    }

    public static ICollection<long> GetEventsByEventTypeIDPaged(int eventTypeID, long startRowIndex, long maximumRows)
    {
        if (eventTypeID == default(int)) 
            throw new ArgumentException("Parameter 'eventTypeID' cannot be null, empty or the default value.");
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventTypeID", eventTypeID),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "Events_GetEventIDsByEventTypeID_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEventsByEventTypeID(int eventTypeID)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventTypeID", eventTypeID),
        };

        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEventsByEventTypeID",
            queryParameters: queryParameters
        );
    }

    public static ICollection<long> GetEventsByEventTypeIDBetweenDatesPaged(int eventTypeID, DateTime startDate, DateTime endDate, long startRowIndex, long maximumRows)
    {
        if (eventTypeID == default(int)) 
            throw new ArgumentException("Parameter 'eventTypeID' cannot be null, empty or the default value.");
        if (startDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'startDate' cannot be null, empty or the default value.");
        if (endDate == default(DateTime)) 
            throw new ArgumentException("Parameter 'endDate' cannot be null, empty or the default value.");
        if (startRowIndex < 0)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventTypeID", eventTypeID),
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "Events_GetEventIDsByEventTypeIDBetweenDates_Paged",
            queryParameters
        );
    }

    public static int GetTotalNumberOfEventsByEventTypeIDBetweenDates(int eventTypeID, DateTime startDate, DateTime endDate)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@EventTypeID", eventTypeID),
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
        };

        return _Database.GetCount<int>(
            "Events_GetTotalNumberOfEventsByEventTypeIDBetweenDates",
            queryParameters: queryParameters
        );
    }
}