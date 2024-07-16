using CloudNimble.ApplicationInsights.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.TelemetryPipeline
{

    /// <summary>
    /// 
    /// </summary>
    internal class OnlineRequestPublisher : IRequestPublisher
    {

        #region Private Members

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelemetryOptions _telemetryOptions;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="telemetryOptions"></param>
        public OnlineRequestPublisher(IHttpClientFactory httpClientFactory, TelemetryOptions telemetryOptions)
        {
            _httpClientFactory = httpClientFactory;
            _telemetryOptions = telemetryOptions;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task PublishAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            var client = _httpClientFactory.CreateClient("ApplicationInsights");
            var response = await client.PostAsJsonAsync(_telemetryOptions.IngestionEndpoint, details);
        }

        #endregion

    }

}
