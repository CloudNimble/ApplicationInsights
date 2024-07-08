using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.WorkerService;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace CloudNimble.ApplicationInsights.Samples.Console.Legacy
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Create the DI container.
            IServiceCollection services = new ServiceCollection();

            // Being a regular console app, there is no appsettings.json or configuration providers enabled by default.
            // Hence instrumentation key/ connection string and any changes to default logging level must be specified here.
            services.AddLogging(loggingBuilder => loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>("Category", LogLevel.Information));
            services.AddApplicationInsightsTelemetryWorkerService((options) => options.ConnectionString = "InstrumentationKey=d129565f-a072-42bc-a74e-0721c8a0a8ee;IngestionEndpoint=https://eastus2-3.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus2.livediagnostics.monitor.azure.com/;ApplicationId=12a445ae-d397-47b6-af73-153a312f6174");

            // To pass a connection string
            // - aiserviceoptions must be created
            // - set connectionstring on it
            // - pass it to AddApplicationInsightsTelemetryWorkerService()

            // Build ServiceProvider.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Obtain logger instance from DI.
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            // Obtain TelemetryClient instance from DI, for additional manual tracking or to flush.
            var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

            var httpClient = new HttpClient();
            var i = 0;

            while (i <= 5) // This app runs indefinitely. Replace with actual application termination logic.
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Replace with a name which makes sense for this operation.
                using (telemetryClient.StartOperation<RequestTelemetry>("operation"))
                {
                    logger.LogWarning("A sample warning message. By default, logs with severity Warning or higher is captured by Application Insights");
                    logger.LogInformation("Calling bing.com");
                    var res = await httpClient.GetAsync("https://bing.com");
                    logger.LogInformation("Calling bing completed with status:" + res.StatusCode);
                    telemetryClient.TrackEvent("Bing call event completed");
                }

                await Task.Delay(1000);
                i++;
            }

            // Explicitly call Flush() followed by sleep is required in console apps.
            // This is to ensure that even if application terminates, telemetry is sent to the back-end.
            telemetryClient.Flush();
            Task.Delay(5000).Wait();
        }
    }
}
