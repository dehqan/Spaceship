﻿namespace Spaceship.Services.Contracts
{
    public interface ICacheProvider
    {
        T GetFromCache<T>(string key) where T : class;
        void SetCache<T>(string key, T value) where T : class;
        void ClearCache(string key);
    }
}
