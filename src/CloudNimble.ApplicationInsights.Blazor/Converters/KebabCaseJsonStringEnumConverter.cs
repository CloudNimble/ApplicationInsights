using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ApplicationInsights.Blazor.Converters
{

    /// <summary>
    /// A <see cref="JsonStringEnumConverter{TEnum}"/> that converts enum values to and from kebab-case strings.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to convert to / from.</typeparam>
    /// <remarks>
    /// This converter is used to properly serialize / deserialize enums coming from built-in browser APIs.
    /// </remarks>
    public class KebabCaseJsonStringEnumConverter<TEnum> : JsonStringEnumConverter<TEnum> where TEnum : struct, Enum
    {

        /// <summary>
        /// The default constructor, for use when constructed via attributes.
        /// </summary>
        public KebabCaseJsonStringEnumConverter() : base(JsonNamingPolicy.KebabCaseLower)
        {
        }

    }

}
