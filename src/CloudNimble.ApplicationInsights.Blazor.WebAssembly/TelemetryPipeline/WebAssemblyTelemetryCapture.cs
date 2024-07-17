using CloudNimble.ApplicationInsights.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.WebAssembly.TelemetryPipeline
{

    /// <summary>
    /// Captures WebAssembly-specific metrics from the <see cref="IWebAssemblyHostEnvironment" />.
    /// </summary>
    public class WebAssemblyTelemetryCapture : ITelemetryCapture
    {

        #region Private Members

        private readonly IWebAssemblyHostEnvironment _hostEnvironment;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="WebAssemblyTelemetryCapture" /> class.
        /// </summary>
        /// <param name="hostEnvironment">The <see cref="IWebAssemblyHostEnvironment" /> instance from the DI container.</param>
        public WebAssemblyTelemetryCapture(IWebAssemblyHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="details"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CaptureAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            details.Tags.TryAdd("ai.cloud.environment", _hostEnvironment.Environment);
            details.Tags.TryAdd("ai.cloud.location", _hostEnvironment.BaseAddress);
            await Task.CompletedTask;
        }

        #endregion

    }
}
