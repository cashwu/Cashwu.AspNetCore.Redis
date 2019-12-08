using System;
using System.Threading.Tasks;

namespace Cashwu.AspNetCore.Redis
{
    public interface IRedisClient
    {
        Task<T> GetAsync<T>(string key)
            where T : class;

        Task<bool> SetAsync<T>(string key, T t)
            where T : class;

        Task<bool> SetAsync<T>(string key, T t, TimeSpan expiry)
            where T : class;    }
}