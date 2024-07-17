using CloudNimble.ApplicationInsights.Models;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITelemetryCapture
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task CaptureAsync<T>(RequestDetails<T> details) where T : InsightsBase;

    }

}
