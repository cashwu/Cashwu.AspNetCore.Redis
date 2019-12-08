using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cashwu.AspNetCore.Redis
{
    public static class SchedulerExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, CommandMap commandMap = null, Version version = null)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var redisConfig = configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>();

                if (redisConfig == null)
                {
                    throw new Exception($"{nameof(RedisConfig)} must not be null");
                }
                
                if (string.IsNullOrEmpty(redisConfig.Ip))
                {
                    throw new Exception($"{nameof(RedisConfig)}.{nameof(RedisConfig.Ip)} must not be null or empty");
                }
                
                redisConfig.CommandMap = commandMap ?? CommandMap.Default;
                redisConfig.Version = version ?? new Version(5, 0);

                return redisConfig;
            });

            services.AddSingleton<IRedisClient, RedisClient>();

            return services;
        }
    }
}