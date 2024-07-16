using CloudNimble.ApplicationInsights;
using CloudNimble.ApplicationInsights.Blazor;
using CloudNimble.ApplicationInsights.Blazor.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting
{

    /// <summary>
    /// Extensions for registering Azure Application Insights services with a Blazor WebAssembly application.
    /// </summary>
    public static class ApplicationInsights_ApplicationInsights_WebAssemblyHostBuilderExtensions
    {

        /// <summary>
        /// Configures Azure Application Insights and related services for use in a Blazor WebAssembly application.
        /// </summary>
        /// <param name="builder">The <see cref="WebAssemblyHostBuilder" /> instance to extend.</param>
        /// <param name="configSectionName">
        /// The name of the section in appsettings.json to get the <see cref="TelemetryOptions" /> from. 
        /// Defaults to "Azure:ApplicationInsights".
        /// </param>
        public static TelemetryClientBuilder AddApplicationInsightsBlazor(this WebAssemblyHostBuilder builder, 
            string configSectionName = "Azure:ApplicationInsights")
        {
            builder.Services.Configure<BlazorTelemetryOptions>(builder.Configuration.GetSection(configSectionName));

            // RWM: Make sure both types of TelemetryOptions are available to the DI container by using the one that already exists.
            builder.Services.AddSingleton(provider => provider.GetService<IOptions<BlazorTelemetryOptions>>().Value);
            builder.Services.AddSingleton<TelemetryOptions>(provider => provider.GetService<IOptions<BlazorTelemetryOptions>>().Value);

            builder.Services.AddSingleton<TelemetryClient>();

            //builder.Services.Remove(builder.Services.GetService<ITelemetryInitializer>());

            return new TelemetryClientBuilder(builder.Services)
                .AddBrowserTelemetry();
        }

    }

}
