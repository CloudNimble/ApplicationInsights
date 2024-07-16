using CloudNimble.ApplicationInsights.Models;

namespace CloudNimble.ApplicationInsights.Blazor
{

    /// <summary>
    /// 
    /// </summary>
    public class BlazorTelemetryOptions : TelemetryOptions
    {

        /// <summary>
        /// 
        /// </summary>
        public SeverityLevel DefaultUnhandledExceptionSeverityLevel { get; set; } = SeverityLevel.Error;

        /// <summary>
        /// 
        /// </summary>
        public bool TrackUnhandledJSExceptions { get; set; } = true;

    }

}
