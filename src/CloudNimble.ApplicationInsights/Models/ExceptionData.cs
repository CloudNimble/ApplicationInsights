using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// An instance of Exception represents a handled or unhandled exception that occurred during execution of the monitored application.
    /// </summary>
    public record ExceptionData : MeasurementsBase
    {

        #region Public Properties

        /// <summary>
        /// Exception chain - list of inner exceptions.
        /// </summary>
        public List<ExceptionDetails> Exceptions { get; set; }

        /// <summary>
        /// Identifier of where the exception was thrown in code. Used for exceptions grouping. 
        /// Typically a combination of exception type and a function from the call stack.
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// Severity level. Mostly used to indicate exception severity level when it is reported by logging library.
        /// </summary>
        public SeverityLevel? SeverityLevel { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        [JsonConstructor]
        private ExceptionData()
        {
            Exceptions = [];
            Measurements = new ConcurrentDictionary<string, double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="severityLevel"></param>
        public ExceptionData(Exception exception, SeverityLevel severityLevel = Models.SeverityLevel.Error) : this()
        {
            SeverityLevel = severityLevel;
            ProblemId = exception.Message;
            var currentException = exception.Demystify();
            while (currentException != null)
            {
                Exceptions.Add(new ExceptionDetails(currentException));
                currentException = currentException.InnerException;
            }
        }

        #endregion

    }

}
