using CloudNimble.ApplicationInsights.Interfaces;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.MetricsCapture
{

    /// <summary>
    /// 
    /// </summary>
    internal class BrowserMetricsCapture : IMetricsCapture
    {

        #region Private Members

        private readonly ApplicationInsightsBrowserInterop _interop;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interop"></param>
        public BrowserMetricsCapture(ApplicationInsightsBrowserInterop interop)
        {
            _interop = interop;
        }

        #endregion

        public async Task CaptureAsync()
        {
            await Task.CompletedTask;
        }

    }

}
