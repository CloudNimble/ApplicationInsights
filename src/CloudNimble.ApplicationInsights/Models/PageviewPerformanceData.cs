using System;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public record PageviewPerformanceData : MeasurementsBase
    {

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly DomProcessing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly NetworkConnect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly PerfTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly ReceivedResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly SentRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

    }

}
