using CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline;
using KristofferStrube.Blazor.Window;
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
            builder.Services.AddSingleton<IWindowService, WindowService>();
            builder.Services.AddSingleton<BrowserTelemetryInterop>();
            builder.Services.AddSingleton<TelemetryClientInterop>();
            builder.Services.AddSingleton<ITelemetryCapture, BrowserTelemetryCapture>();
            return builder;
        }

    }

}
