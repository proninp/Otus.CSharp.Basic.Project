using FinanceManager.Core.Options;
using FinanceManager.Redis.Services;
using FinanceManager.Redis.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FinanceManager.CompositionRoot;
public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            var configuration = ConfigurationOptions.Parse(options.RedisConnectionString, true);
            configuration.Password = options.RedisPassword;

            return ConnectionMultiplexer.Connect(configuration);
        });

        services
            .AddTransient<IRedisCacheService, RedisCacheService>()
            .AddTransient<IRedisInitializer, RedisInitializer>();

        return services;
    }
}