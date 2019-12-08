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
    }
}