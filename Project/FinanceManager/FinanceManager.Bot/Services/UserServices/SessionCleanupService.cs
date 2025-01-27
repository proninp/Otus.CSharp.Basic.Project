using FinanceManager.Bot.Services.Interfaces.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMinutes(10));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _services.CreateScope();
                var userSessionManager = scope.ServiceProvider.GetRequiredService<IUserSessionManager>();

                int cleanedSessions = await userSessionManager.CleanupExpiredSessions(stoppingToken);
                _logger.Information($"Cleaned {cleanedSessions} expired sessions.");

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while cleaning up sessions.");
            }
        }
        _logger.Information("Session Cleanup Service is stopping.");
    }
}
