# Asp.Net Core redis client

[![actions](https://github.com/cashwu/Cashwu.AspNetCore.Redis/workflows/.NET%20Core/badge.svg?branch=master)](https://github.com/cashwu/Cashwu.AspNetCore.Redis/actions)

---

[![Nuget](https://img.shields.io/badge/Nuget-Cashwu.AspnetCore.Redis-blue.svg)](https://www.nuget.org/packages/Cashwu.AspnetCore.Redis)

---

## RedisConfig in `appsetting.json`

```json
"RedisConfig": {
  "Password": "password",
  "SyncTimeout": 5000,
  "ConnectTimeout": 3000,
  "Prefix": "refix_",
  "Ip": "127.0.0.1",
  "Port": 6379
}
```

## Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddRedis();
}
```

## Using by inject

```csharp
public class Test
{
    private readonly IRedisClient _redisClient;

    public Test(IRedisClient redisClient)
    {
        _redisClient = redisClient;
    }
}
```
