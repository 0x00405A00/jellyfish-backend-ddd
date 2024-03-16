using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Cache
{
    public class CachingHandler : ICachingHandler
    {
        private readonly ILogger<CachingHandler> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public CachingHandler(
            ILogger<CachingHandler> logger,
            IMemoryCache memoryCache,
            IDistributedCache distributedCache)
        {
            this._logger = logger;
            this._memoryCache = memoryCache;
            this._distributedCache = distributedCache;
        }

        public async Task<T> GetObjectByKey<T>(string key)
        {
            var distributedCacheData = await _distributedCache.GetAsync(key);
            if (distributedCacheData != null)
            {
                return JsonSerializer.Deserialize<T>(distributedCacheData);
            }

            if (_memoryCache.TryGetValue(key, out T memoryCacheData))
            {
                return memoryCacheData;
            }

            return default;
        }
        public async Task<T> GetOrSet<T>(string key, Func<Task<T>> valueFactory, DistributedCacheEntryOptions distributedOptions = null, MemoryCacheEntryOptions memoryOptions = null)
        {
            var getValue = await GetObjectByKey<T>(key);
            if (getValue != null)
            {
                return getValue;
            }
            // If not found in either cache, set the value using valueFactory
            var value = await valueFactory();

            // Store the value in both caches if options provided
            await Set<T>(key, value);

            // Return the value
            return value;
        }
        public async Task Set<T>(string key, T value, DistributedCacheEntryOptions distributedOptions = null, MemoryCacheEntryOptions memoryOptions = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }

            if (distributedOptions != null)
            {
                await _distributedCache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(value), distributedOptions);
            }

            if (memoryOptions != null)
            {
                _memoryCache.Set(key, value, memoryOptions);
            }
        }
    }
}
