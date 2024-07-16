using CloudNimble.ApplicationInsights.TelemetryPipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public class TelemetryClientBuilder
    {

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public TelemetryClientBuilder(IServiceCollection services)
        {
            Services = services;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TelemetryClientBuilder SendImmediately()
        {
            Services.AddSingleton<IRequestPublisher, OnlineRequestPublisher>();
            Services.AddHttpClient("ApplicationInsights");
            return this;
        }

        #endregion

    }

}
