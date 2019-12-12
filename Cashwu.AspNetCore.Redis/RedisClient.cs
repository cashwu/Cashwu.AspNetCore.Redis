using System;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Cashwu.AspNetCore.Redis
{
    public class RedisClient : IRedisClient
    {
        public RedisClient(RedisConfig redisConfig)
        {
            RedisConnection.Init(redisConfig);
        }

        public IDatabase Database => RedisConnection.Database;

        public ConnectionMultiplexer Connection => RedisConnection.Connection;
        
        public async Task<T> GetAsync<T>(string key)
            where T : class
        {
            var redisValue = await RedisConnection.Database.StringGetAsync(key);

            if (redisValue == RedisValue.Null || redisValue == RedisValue.EmptyString)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(redisValue);
        }

        public Task<bool> SetAsync<T>(string key, T t)
            where T : class
        {
            return RedisConnection.Database.StringSetAsync(key, JsonSerializer.Serialize(t));
        }

        public Task<bool> SetAsync<T>(string key, T t, TimeSpan expiry)
            where T : class
        {
            return RedisConnection.Database.StringSetAsync(key, JsonSerializer.Serialize(t), expiry);
        }

        public Task HashSetAsync(string key, HashEntry[] hasSet)
        {
            return RedisConnection.Database.HashSetAsync(key, hasSet);
        }

        public Task HashSetAsync(string key, RedisValue hashField, RedisValue value)
        {
            return RedisConnection.Database.HashSetAsync(key, hashField, value);
        }

        public Task<RedisValue> HashGetAsync(string key, RedisValue hashField)
        {
            return RedisConnection.Database.HashGetAsync(key, hashField);
        }

        public T HashGet<T>(string key, RedisValue hashField, Func<RedisValue, T> func = null)
        {
            var redisValue = RedisConnection.Database.HashGet(key, hashField);

            if (func != null)
            {
                return func.Invoke(redisValue);
            }

            return (T)Convert.ChangeType(redisValue, typeof(T));
        }

        public Task<bool> KeyDeleteAsync(string key)
        {
            return RedisConnection.Database.KeyDeleteAsync(key);
        }
    }
}