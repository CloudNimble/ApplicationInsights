using System;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public record RemoteDependencyData : MeasurementsBase
    {

        /// <summary>
        /// The result code of a dependency call. 
        /// </summary>
        /// <remarks>
        /// It's the HTTP status code for HTTP requests. It might be an HRESULT value or an exception type for other request types.
        /// </remarks>
        public string ResultCode { get; set; }

        /// <summary>
        /// Request telemetry represents the operation with the beginning and the end.
        /// </summary>
        public TimeOnly Duration { get; set; }

        /// <summary>
        /// The identifier of a dependency call instance. It's used for correlation with the request telemetry item that 
        /// corresponds to this dependency call
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Success indicates whether a call was successful or unsuccessful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

    }

}
