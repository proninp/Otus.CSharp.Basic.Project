using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class SessionCleanupService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;

    public SessionCleanupService(IServiceProvider services, ILogger logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Information("Session Cleanup Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();

                var options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<AppSettings>>();
                var userSessionManager = scope.ServiceProvider.GetRequiredService<ISessionManager>();

                int cleanedSessionsCount = await userSessionManager.CleanupExpiredSessions(stoppingToken);
                _logger.Information($"Cleaned {cleanedSessionsCount} expired sessions.");

                var interval = options.Value.SessionCleanupIntervalMinutes;
                await Task.Delay(TimeSpan.FromMinutes(interval), stoppingToken);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while cleaning up sessions.");
            }
        }
        _logger.Information("Session Cleanup Service is stopping.");
    }
}
