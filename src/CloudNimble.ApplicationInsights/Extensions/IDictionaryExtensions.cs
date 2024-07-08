using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{

    /// <summary>
    /// 
    /// </summary>
    public static class IDictionaryExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <remarks>
        /// For concurrency's sake, we should probably copy values into dictionaries instead of modifying the existing ones.
        /// However, loading a new dictionary with existing items will not add new values to the existing dictionary, so this
        /// might not actually be necessary.
        /// </remarks>
        public static void CopyTo<TValue>(this IDictionary<string, TValue> source, IDictionary<string, TValue> destination)
        {
            foreach (var item in source.Where(c => !string.IsNullOrWhiteSpace(c.Key)))
            {
                destination.TryAdd(item.Key, item.Value);
            }
        }

    }

}
