using System.Collections.Generic;

#if NETSTANDARD2_0

namespace SharpApi
{
    /// <summary>
    /// Extensions for <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Deconstructs a <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="kvp"><see cref="KeyValuePair{TKey, TValue}"/> to deconstruct.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
    }
}

#endif
