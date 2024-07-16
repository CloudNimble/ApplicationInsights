using CloudNimble.ApplicationInsights.Blazor.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CloudNimble.ApplicationInsights.Samples.Blazor.WebAssembly
{

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.AddApplicationInsightsBlazor().SendImmediately();

            await builder.Build().RunAsync();
        }

    }

}
