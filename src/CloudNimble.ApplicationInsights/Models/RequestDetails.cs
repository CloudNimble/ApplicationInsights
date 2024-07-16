using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class RequestDetails<T> where T : InsightsBase
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public InsightsDataWrapper<T> Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("iKey")]
        public Guid InstrumentationKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Tags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Time { get; set; }

        #endregion

        #region Constructors

        [JsonConstructor]
        private RequestDetails()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instrumentationKey"></param>
        /// <param name="data"></param>
        public RequestDetails(Guid instrumentationKey, T data)
        {
            Data = new InsightsDataWrapper<T>(data);
            InstrumentationKey = instrumentationKey;
            Name = $"Microsoft.ApplicationInsights.{InstrumentationKey:N}.{typeof(T).Name[..^4]}";
            Tags = [];
            Time = DateTimeOffset.UtcNow;
        }

        #endregion

    }

}
