using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    public record MeasurementsBase : InsightsBase
    {

        /// <summary>
        /// Collection of custom measurements.
        /// </summary>
        public IDictionary<string, double> Measurements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MeasurementsBase() : base()
        {
            Measurements = new Dictionary<string, double>();
        }

    }

}
