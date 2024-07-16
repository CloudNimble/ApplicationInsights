using CloudNimble.ApplicationInsights.Blazor.Offline;
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
        public static TelemetryClientBuilder AddOfflineSupport(this TelemetryClientBuilder builder)
        {
            // RWM: This is probably not right and we'll have to figure out how to
            //      deal with network availability in a more robust way.
            builder.Services.AddSingleton<IRequestPublisher, OfflineRequestPublisher>();
            return builder;
        }

    }

}
