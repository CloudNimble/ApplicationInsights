using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.Extensions
{

    /// <summary>
    /// 
    /// </summary>
    public static class TelemetryClientBuilderExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static TelemetryClientBuilder AddOfflineSupport(this TelemetryClientBuilder builder)
        {
            //builder.Services.AddSingleton<ApplicationInsightsOfflineSupport>();
            return builder;
        }

    }

}
