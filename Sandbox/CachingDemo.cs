using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    public interface ICache
    {
        void Add(string key, object value);
        void Add(string key, object value, DateTime expiration);
        void Add(string key, object value, TimeSpan duration);
        object Get(string key);
        void Remove(string key);
        void Clear();
    }

    public class AspNetCache : ICache
    {
        public void Add(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public void Add(string key, object value, DateTime expiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, expiration, Cache.NoSlidingExpiration);
        }

        public void Add(string key, object value, TimeSpan duration)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, duration);
        }

        public object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Clear()
        {
            HttpRuntime.Close();
        }
    }

    public class NullCache : ICache
    {
        public void Add(string key, object value)
        {
        }

        public void Add(string key, object value, DateTime expiration)
        {
        }

        public void Add(string key, object value, TimeSpan duration)
        {
        }

        public object Get(string key)
        {
            return null;
        }

        public void Remove(string key)
        {
        }

        public void Clear()
        {
        }
    }

    public class InProcessCache : ICache
    {
        private readonly Dictionary<string, CacheItem> storage = new Dictionary<string, CacheItem>();
  
        public void Add(string key, object value)
        {
            Add(key, value, DateTime.MaxValue);
        }

        public void Add(string key, object value, DateTime expiration)
        {
            storage.Add(key, new CacheItem(value, expiration));
        }

        public void Add(string key, object value, TimeSpan duration)
        {
            Add(key, value, DateTime.UtcNow + duration);
        }

        public object Get(string key)
        {
            if (storage.ContainsKey(key))
            {
                CacheItem cacheItem = storage[key];
                if (!cacheItem.IsExpired)
                {
                    return cacheItem.Value;
                }

                // The cached item has expired.
                storage.Remove(key);
                return null;
            }
            return null;
        }

        public void Remove(string key)
        {
            if (storage.ContainsKey(key))
            {
                storage.Remove(key);
            }
        }

        public void Clear()
        {
            storage.Clear();
        }

        internal class CacheItem
        {
            private readonly DateTime expiration;
            private readonly object value;

            public CacheItem(object value, DateTime expiration)
            {
                this.value = value;
                this.expiration = expiration;
            }

            public DateTime Expiration
            {
                get { return expiration; }
            }

            public bool IsExpired
            {
                get { return DateTime.UtcNow > expiration; }
            }

            public object Value
            {
                get { return value; }
            }
        }
    }

    [TestFixture]
    public class InProcessCache_Tests
    {
        private ICache cache;

        [SetUp]
        public void before_each_test()
        {
            CreateNewCache();
        }

        [TearDown]
        public void after_each_test()
        {
            DestroyCache();
        }

        [Test]
        public void can_add_an_item_that_expires_in_the_future()
        {
            
        }

        [Test]
        public void can_add_and_read_items_from_cache()
        {
            cache.Add("one", 1);
            object value = cache.Get("one");
            value.Should().Be(1);
        }

        [Test]
        public void expired_items_return_null()
        {
            cache.Add("one", 1, TimeSpan.FromSeconds(-1));
            object value = cache.Get("one");
            value.Should().BeNull();
        }

        private void CreateNewCache()
        {
            cache = new InProcessCache();
        }

        private void DestroyCache()
        {
            cache = null;
        }
    }
}
