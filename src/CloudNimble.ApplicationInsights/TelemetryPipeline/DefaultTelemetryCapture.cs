using CloudNimble.ApplicationInsights.Models;
using System.Diagnostics;
using System.Reflection;
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
            details.Tags.TryAdd("ai.operation.id", ActivitySpanId.CreateRandom().ToHexString());
            details.Tags.TryAdd("ai.internal.sdkVersion", $"net8:{Assembly.GetExecutingAssembly()
                              .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                              ?.InformationalVersion ?? "1.0.0"}");
            await Task.CompletedTask;
        }

    }

}
