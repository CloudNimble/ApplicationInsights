using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Interfaces
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
        public async Task PublishAsync()
        {
            await Task.CompletedTask;
        }

    }

}
