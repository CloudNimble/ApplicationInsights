using CloudNimble.ApplicationInsights.Blazor.Models;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Originally, we wanted to keep all the interop in one class. However, that created a circular reference in the
    /// <see cref="TelemetryClient" /> when we add the <see cref="BrowserTelemetryCapture" /> pipeline to the DI container.
    /// So we had to separate out capturing metrics from providing a way to call <see cref="TelemetryClient" /> from JavaScript.
    /// </remarks>
    public class BrowserTelemetryInterop : IAsyncDisposable
    {

        #region Private Members

        private readonly IJSRuntime _jsRuntime;

        #endregion

        #region Internal Properties

        /// <summary>
        /// A dynamic reference to the Application Insights Blazor Script.
        /// </summary>
        internal TelemetryJsModule ApplicationInsightsScript { get; private set; }

        /// <summary>
        /// Details about the Browser that are highly unlikely to change at runtime.
        /// </summary>
        internal BrowserSpecs BrowserSpecs { get; private set; }

        /// <summary>
        /// Details about the Browser that are retrieved on every error report.
        /// </summary>
        internal BrowserStats LatestBrowserStats { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="BrowserTelemetryInterop" /> class.
        /// </summary>
        /// <param name="jsRuntime">The <see cref="IJSRuntime" /> instance to use for JS Interop.</param>
        public BrowserTelemetryInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            ApplicationInsightsScript = new(_jsRuntime);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Properly configures BlazorInsights Blazor for use with JavaScript.
        /// </summary>
        public async Task InitializeAsync()
        {
            // RWM: We're going to register the ApplicationInsights script and get the BrowserSpecs first. The reason why is
            //      because if we handle JS errors & they start coming in before we're ready, then there will be wailing and
            //      gnashing of teeth.
            if (BrowserSpecs is not null) return;

            // RWM: Get and cache the BrowserSpecs so we can use them later.
            BrowserSpecs = await ApplicationInsightsScript.GetBrowserSpecs();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (ApplicationInsightsScript is not null)
            {
                await ApplicationInsightsScript.DisposeAsync();
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task UpdateBrowserStats()
        {
            LatestBrowserStats ??= await ApplicationInsightsScript.GetBrowserStats();
        }

        #endregion

    }

}
