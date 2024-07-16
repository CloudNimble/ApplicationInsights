using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// Specifies the options available for configuring the <see cref="TelemetryClient" />.
    /// </summary>
    public class TelemetryOptions
    {

        #region Private Members

        private Guid _applicationId;
        private string _connectionString;
        private Dictionary<string, string> _connectionStringData = [];
        private Guid _instrumentationKey;

        #endregion

        /// <summary>
        /// A read-only string specifying the ApplicationId for the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public Guid ApplicationId =>
            _applicationId == Guid.Empty ?
            _applicationId = Guid.Parse(_connectionStringData[nameof(ApplicationId)]) :
            _applicationId;

        /// <summary>
        /// The ConnectionString for the Application Insights instance from the Azure dashboard.
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                _connectionStringData = _connectionString.Split(';')
                    .Select(c => c.Split('='))
                    .ToDictionary(split => split[0], split => split[1]);
            }
        }

        /// <summary>
        /// A read-only string specifying the Endpoint for ingesting data into the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string IngestionEndpoint => $"{_connectionStringData[nameof(IngestionEndpoint)]}v2/track";

        /// <summary>
        /// A read-only string specifying the InstrumentationKey for the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public Guid InstrumentationKey =>
            _instrumentationKey == Guid.Empty ?
            _instrumentationKey = Guid.Parse(_connectionStringData[nameof(InstrumentationKey)]) :
            _instrumentationKey;

        /// <summary>
        /// A read-only string specifying the Endpoint for sending real-time data to the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string LiveEndpoint => _connectionStringData[nameof(LiveEndpoint)];

        /// <summary>
        /// Specifies whether or not to track unhandled exceptions in .NET. Defaults to <see langword="true" />.
        /// </summary>
        public bool TrackUnhandledExceptions { get; set; } = true;

    }

}
