using System;
using System.Runtime.Caching;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class Cache<T> where T: class
    {
        public T GetValue(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(id.ToString()) as T;
        }

        public T GetValue(string name)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(name) as T;
        }

        public bool Add(T value, int id)
        {            
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public bool Add(T value, string name)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(name, value, DateTime.Now.AddMinutes(10));
        }

        public void Update(T value, int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void Delete(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }
    }
}