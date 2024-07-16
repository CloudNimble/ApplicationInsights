using System;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public record PageviewData : MeasurementsBase
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public TimeOnly Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The request URL with all query string parameters.
        /// </summary>
        public string Url { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="duration"></param>
        public PageviewData(string url, TimeOnly duration)
        {
            Url = url;
            Duration = duration;
        }

        #endregion

    }

}
