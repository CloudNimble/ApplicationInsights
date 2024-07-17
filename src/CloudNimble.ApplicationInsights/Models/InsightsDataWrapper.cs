using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[JsonPolymorphic(TypeDiscriminatorPropertyName = "baseType")]
    public record InsightsDataWrapper<T> where T : InsightsBase
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public T BaseData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BaseType { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        [JsonConstructor]
        internal InsightsDataWrapper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public InsightsDataWrapper(T data)
        {
            BaseData = data;
            BaseType = typeof(T).Name;
        }

        #endregion

    }

}
