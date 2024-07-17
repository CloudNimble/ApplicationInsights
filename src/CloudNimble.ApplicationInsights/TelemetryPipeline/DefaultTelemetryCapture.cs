using CloudNimble.ApplicationInsights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.TelemetryPipeline
{


    /// <summary>
    /// 
    /// </summary>
    public class DefaultTelemetryCapture : ITelemetryCapture
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task CaptureAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            details.Tags.TryAdd("ai.internal.sdkVersion", $"net8:{Assembly.GetExecutingAssembly()
                              .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                              ?.InformationalVersion ?? "1.0.0"}");
            await Task.CompletedTask;
        }

    }

}
