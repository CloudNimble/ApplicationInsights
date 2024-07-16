namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public record MessageData : InsightsBase
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Severity level. Mostly used to indicate exception severity level when it is reported by logging library.
        /// </summary>
        public SeverityLevel? SeverityLevel { get; set; }

        #endregion

    }

}
