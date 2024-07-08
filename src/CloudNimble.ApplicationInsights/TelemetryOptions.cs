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

        private string _connectionString;
        private Dictionary<string, string> connectionStringData = [];

        #endregion

        /// <summary>
        /// A read-only string specifying the ApplicationId for the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string ApplicationId => connectionStringData[nameof(ApplicationId)];

        /// <summary>
        /// The ConnectionString for the Application Insights instance from the Azure dashboard.
        /// </summary>
        public string ConnectionString 
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                connectionStringData = _connectionString.Split(';')
                    .Select(c => c.Split('='))
                    .ToDictionary(split => split[0], split => split[1]);
            }
        }

        /// <summary>
        /// A read-only string specifying the Endpoint for ingesting data into the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string IngestionEndpoint => connectionStringData[nameof(IngestionEndpoint)];

        /// <summary>
        /// A read-only string specifying the InstrumentationKey for the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string InstrumentationKey => connectionStringData[nameof(InstrumentationKey)];

        /// <summary>
        /// A read-only string specifying the Endpoint for sending real-time data to the Application Insights instance, specified in the ConnectionString.
        /// </summary>
        public string LiveEndpoint => connectionStringData[nameof(LiveEndpoint)];

    }

}
