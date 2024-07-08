using CloudNimble.ApplicationInsights;
using CloudNimble.ApplicationInsights.Blazor;
using KristofferStrube.Blazor.Window;
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
        public static void AddApplicationInsightsApplicationInsights(this WebAssemblyHostBuilder builder, 
            string configSectionName = "Azure:ApplicationInsights")
        {
            builder.Services.AddSingleton<ApplicationInsightsBrowserInterop>();
            builder.Services.AddSingleton<IWindowService, WindowService>();

            builder.Services.Configure<TelemetryOptions>(builder.Configuration.GetSection(configSectionName));
            builder.Services.AddSingleton(provider => provider.GetService<IOptions<TelemetryOptions>>().Value);
            builder.Services.AddSingleton<TelemetryClient>();
        }

    }

}
