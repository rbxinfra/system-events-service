namespace Roblox.SystemEvents;

using System.ComponentModel;

/// <summary>
/// Represents the different errors returned by System Events.
/// </summary>
internal enum SystemEventsErrors
{
    /// <summary>
    /// Error when the type is not specified in the log event request
    /// </summary>
    [Description("The type was not specifed in the request")]
    TypeNotSpecified,

    /// <summary>
    /// Error when the summary is not specified in the log event request
    /// </summary>
    [Description("The summary was not specifed in the request")]
    SummaryNotSpecified,

    /// <summary>
    /// Error when the end date is specified but there is no start date.
    /// </summary>
    [Description("Start date must be specified alongside end date.")]
    StartDateMustBeSpecifiedWithEndDate,

    /// <summary>
    /// Error when the summary is longer that <see cref="ISettings.MaximumEventSummaryLength"/>
    /// </summary>
    [Description("Summary must not exceed {0} characters in length!")]
    SummaryLengthTooLong
}