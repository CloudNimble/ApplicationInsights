using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// A base class that contains the common properties for every request sent to Application Insights.
    /// </summary>
    public class InsightsBase
    {

        /// <summary>
        /// Schema version. Defaults to 2.
        /// </summary>
        [JsonPropertyName("ver")]
        public int Version { get; set; } = 2;




    }

}
