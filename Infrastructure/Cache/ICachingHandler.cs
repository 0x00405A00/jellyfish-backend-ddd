using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache
{
    public interface ICachingHandler
    {
        Task<T> GetObjectByKey<T>(string key);
        Task<T> GetOrSet<T>(string key, Func<Task<T>> valueFactory, DistributedCacheEntryOptions distributedOptions = null, MemoryCacheEntryOptions memoryOptions = null);
        Task Set<T>(string key, T value, DistributedCacheEntryOptions distributedOptions = null, MemoryCacheEntryOptions memoryOptions = null);
    }
}