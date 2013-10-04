using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Sandbox.Caching
{
    public class AspNetHttpCache : ICache
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

        public T Get<T>(string key)
        {
            var value = Get(key);
            if (value != null)
                return (T)value;

            return default(T);
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            HttpRuntime.Close();
        }
    }
}
