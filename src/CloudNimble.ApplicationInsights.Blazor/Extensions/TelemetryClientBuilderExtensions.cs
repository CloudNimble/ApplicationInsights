using CloudNimble.ApplicationInsights.Blazor.MetricsCapture;
using Microsoft.Extensions.DependencyInjection;

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
        public static TelemetryClientBuilder AddBrowserTelemetry(this TelemetryClientBuilder builder)
        {
            builder.Services.AddSingleton<ApplicationInsightsBrowserInterop>();
            builder.Services.AddSingleton<BrowserMetricsCapture>();
            return builder;
        }

    }

}
