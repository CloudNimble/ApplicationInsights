using CloudNimble.ApplicationInsights.TelemetryPipeline;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudNimble.ApplicationInsights.Extensions
{

    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static TelemetryClientBuilder AddApplicationInsights(this IServiceCollection services, Action<TelemetryOptions> configureOptions)
        {
            services.Configure(configureOptions);

            services.AddSingleton<ITelemetryCapture, DefaultTelemetryCapture>();

            return new TelemetryClientBuilder(services);
        }

    }

}
