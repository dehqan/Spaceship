using System;
using Microsoft.Extensions.Caching.Memory;
using Spaceship.Services.Contracts;

namespace Spaceship.Infrastructure.Cache;

public class CacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cache;

    public CacheProvider(IMemoryCache cache) => _cache = cache;

    public T GetFromCache<T>(string key) where T : class => _cache.Get(key) as T;

    public void SetCache<T>(string key, T value) where T : class => _cache.Set(key, value);

    public void ClearCache(string key) => _cache.Remove(key);
}