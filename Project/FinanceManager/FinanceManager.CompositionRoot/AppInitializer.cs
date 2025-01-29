using FinanceManager.Bot.Services.Telegram.Abstractions;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Redis.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceManager.CompositionRoot;
public sealed class AppInitializer : BackgroundService
{
    private readonly IServiceProvider _services;

    public AppInitializer(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await appDbContext.Database.MigrateAsync(stoppingToken);

        var redisInitializer = scope.ServiceProvider.GetRequiredService<IRedisInitializer>();
        await redisInitializer.ClearDatabase();

        var pollingService = scope.ServiceProvider.GetRequiredService<IPollingService>();
        await pollingService.DoWork(stoppingToken);
    }
}
