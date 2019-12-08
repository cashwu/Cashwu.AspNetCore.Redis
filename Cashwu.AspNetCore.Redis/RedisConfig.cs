using System;
using StackExchange.Redis;

namespace Cashwu.AspNetCore.Redis
{
    public class RedisConfig 
    {
        public string Password { get; set; }

        public int SyncTimeout { get; set; } = 5000;

        public int ConnectTimeout { get; set; } = 3000;

        public string Prefix { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; } = 6379;

        public bool AllowAdmin { get; set; } = true;

        public bool AbortOnConnectFail { get; set; } = false;

        public CommandMap CommandMap { get; set; } = CommandMap.Default;

        public Version Version { get; set; } = new Version(5, 0);
    }
}