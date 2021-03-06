using System;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;

namespace Cashwu.AspNetCore.Redis
{
    internal sealed class RedisConnection
    {
        private static readonly Lazy<RedisConnection> Lazy = new Lazy<RedisConnection>(() => new RedisConnection());

        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private static ConfigurationOptions _redisConfigOptions;
        private static RedisConfig _config;

        private static IDatabase _database;

        public static IDatabase Database
        {
            get
            {
                return _database ??= Connection?.GetDatabase().WithKeyPrefix(_config.Prefix);
            }
        }

        public static ConnectionMultiplexer Connection => Lazy.Value._connectionMultiplexer;

        private RedisConnection()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisConfigOptions);
        }

        public static void Init(RedisConfig config)
        {
            _config = config;

            _redisConfigOptions = new ConfigurationOptions
            {
                CommandMap = config.CommandMap,
                EndPoints = { { config.Ip, config.Port } },
                Password = config.Password,
                AllowAdmin = config.AllowAdmin,
                SyncTimeout = config.SyncTimeout,
                ConnectTimeout = config.ConnectTimeout,
                DefaultVersion = config.Version,
                AbortOnConnectFail = config.AbortOnConnectFail
            };
        }
    }
}