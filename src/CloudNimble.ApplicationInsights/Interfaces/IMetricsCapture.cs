using CloudNimble.ApplicationInsights.Models;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMetricsCapture
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CaptureAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            await Task.CompletedTask;
        }

    }

}
