using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Interfaces
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
        public async Task CaptureAsync()
        {
            await Task.CompletedTask;
        }

    }

}
