using CloudNimble.ApplicationInsights.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public class TelemetryClient
    {

        #region Private Members

        private readonly List<ITelemetryCapture> _metricsCaptureList;
        private readonly Dictionary<string, Stopwatch> _pageStopwatches = [];
        private readonly IRequestPublisher _requestPublisher;
        private readonly TelemetryOptions _telemetryOptions;

        internal static JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="TelemetryClient" /> class.
        /// </summary>
        /// <param name="telemetryOptions"></param>
        /// <param name="metricsCaptureList"></param>
        /// <param name="requestPublisher"></param>
        public TelemetryClient(TelemetryOptions telemetryOptions, IEnumerable<ITelemetryCapture> metricsCaptureList, IRequestPublisher requestPublisher)
        {
            _telemetryOptions = telemetryOptions;
            _metricsCaptureList = metricsCaptureList.ToList();
            _requestPublisher = requestPublisher;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void StartTrackPage(string url)
        {
            _pageStopwatches[url] = Stopwatch.StartNew();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task StopTrackPageAsync(string url)
        {
            _pageStopwatches[url]?.Stop();
            if (!_pageStopwatches.TryGetValue(url, out var stopwatch)) return;

            await SendAsync(new RequestDetails<PageviewData>(_telemetryOptions.InstrumentationKey, 
                new PageviewData(url, new TimeOnly(stopwatch.Elapsed.Ticks))));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        public async Task TrackEventAsync(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            await SendAsync(new RequestDetails<EventData>(_telemetryOptions.InstrumentationKey,
                new EventData()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        public async Task TrackExceptionAsync(Exception exception, SeverityLevel severityLevel = SeverityLevel.Error, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            await SendAsync(new RequestDetails<ExceptionData>(_telemetryOptions.InstrumentationKey,
                new ExceptionData(exception, severityLevel)));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SendAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            foreach (var metricsCapture in _metricsCaptureList)
            {
                await metricsCapture.CaptureAsync(details);
            }
            await _requestPublisher.PublishAsync(details);
        }

        #endregion

    }

}
