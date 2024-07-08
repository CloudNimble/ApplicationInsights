using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new TelemetryClientBuilder(services);
        }

    }

}
