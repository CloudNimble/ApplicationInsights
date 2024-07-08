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

    }

}
