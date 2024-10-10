using CloudNimble.ApplicationInsights.Blazor.Models;
using CloudNimble.BlazorEssentials;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline
{

    /// <summary>
    /// 
    /// </summary>
    internal class TelemetryJsModule : JsModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsRuntime"></param>
        internal TelemetryJsModule(IJSRuntime jsRuntime) : base(jsRuntime)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task<BrowserSpecs> GetBrowserSpecs() => await InvokeAsync<BrowserSpecs>("getBrowserSpecs");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task<BrowserStats> GetBrowserStats() => await InvokeAsync<BrowserStats>("getBrowserStats");

    }

}
