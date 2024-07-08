using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// An instance of Exception represents a handled or unhandled exception that occurred during execution of the monitored application.
    /// </summary>
    public class ExceptionDetails : InsightsBase
    {

        #region Public Properties

        /// <summary>
        /// Exception chain - list of inner exceptions.
        /// </summary>
        public List<ExceptionDetails> Exceptions { get; set; }

        /// <summary>
        /// Collection of custom measurements.
        /// </summary>
        public IDictionary<string, double> Measurements { get; set; }

        /// <summary>
        /// Identifier of where the exception was thrown in code. Used for exceptions grouping. 
        /// Typically a combination of exception type and a function from the call stack.
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// Collection of custom properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Severity level. Mostly used to indicate exception severity level when it is reported by logging library.
        /// </summary>
        public SeverityLevel? SeverityLevel { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ExceptionDetails()
        {
            Exceptions = [];
            Measurements = new ConcurrentDictionary<string, double>();
            Properties = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public ExceptionDetails(Exception exception) : this()
        {
        }

        #endregion

    }

}
