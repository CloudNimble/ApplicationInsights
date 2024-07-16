using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// A base class that contains the common properties for every request sent to Application Insights.
    /// </summary>
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    public abstract record InsightsBase
    {

        /// <summary>
        /// Schema version. Defaults to 2.
        /// </summary>
        [JsonPropertyName("ver")]
        public int Version { get; set; } = 2;

        /// <summary>
        /// Collection of custom properties.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public InsightsBase()
        {
            Properties = [];
        }

    }

}