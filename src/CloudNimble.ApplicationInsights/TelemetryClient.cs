using CloudNimble.ApplicationInsights.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights
{

    /// <summary>
    /// 
    /// </summary>
    public class TelemetryClient
    {

        #region Private Members

        private readonly List<IMetricsCapture> _metricsCaptureList;
        private readonly IRequestPublisher _requestPublisher;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metricsCaptureList"></param>
        /// <param name="requestPublisher"></param>
        public TelemetryClient(List<IMetricsCapture> metricsCaptureList, IRequestPublisher requestPublisher)
        {
            _metricsCaptureList = metricsCaptureList;
            _requestPublisher = requestPublisher;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        public async Task TrackEventAsync(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            await SendAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        public async Task TrackExceptionAsync(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            exception = exception.Demystify();
            await SendAsync();
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SendAsync()
        {
            foreach (var metricsCapture in _metricsCaptureList)
            {
                await metricsCapture.CaptureAsync();
            }
            await _requestPublisher.PublishAsync();
        }

    }

}
