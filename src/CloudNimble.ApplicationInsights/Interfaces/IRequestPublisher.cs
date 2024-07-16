using CloudNimble.ApplicationInsights.Models;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public interface IRequestPublisher
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task PublishAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            await Task.CompletedTask;
        }

    }

}
