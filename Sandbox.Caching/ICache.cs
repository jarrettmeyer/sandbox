using System;

namespace Sandbox.Caching
{
    /// <summary>
    /// Represents a mechanism to store values.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Add an item to the cache.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        void Add(string key, object value);

        void Add(string key, object value, DateTime expiration);

        void Add(string key, object value, TimeSpan duration);

        object Get(string key);

        T Get<T>(string key);

        void Remove(string key);

        void Clear();
    }
}
