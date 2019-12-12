using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Cashwu.AspNetCore.Redis
{
    public interface IRedisClient
    {
        IDatabase Database { get; }

        ConnectionMultiplexer Connection { get; }

        Task<T> GetAsync<T>(string key)
            where T : class;

        Task<bool> SetAsync<T>(string key, T t)
            where T : class;

        Task<bool> SetAsync<T>(string key, T t, TimeSpan expiry)
            where T : class;

        Task HashSetAsync(string key, HashEntry[] hasSet);

        Task HashSetAsync(string key, RedisValue hashField, RedisValue value);

        Task<RedisValue> HashGetAsync(string key, RedisValue hashField);

        T HashGet<T>(string key, RedisValue hashField, Func<RedisValue, T> func = null);

        Task<bool> KeyDeleteAsync(string key);
    }
}