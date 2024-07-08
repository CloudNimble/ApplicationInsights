using CloudNimble.ApplicationInsights.Interfaces;
using System.Net.Http;
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
        public async Task PublishAsync()
        {
            var client = _httpClientFactory.CreateClient("ApplicationInsights");
            var request = new HttpRequestMessage(HttpMethod.Post, _telemetryOptions.IngestionEndpoint);
            var response = await client.SendAsync(request);
        }

        #endregion

    }

}
