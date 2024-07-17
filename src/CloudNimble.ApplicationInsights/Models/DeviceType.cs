using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceType
    {

        /// <summary>
        /// 
        /// </summary>
        PC,

        /// <summary>
        /// 
        /// </summary>
        Phone,

        /// <summary>
        /// 
        /// </summary>
        Browser

    }

}
