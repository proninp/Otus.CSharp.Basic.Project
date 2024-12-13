using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Telegram;
using FinanceManager.Bot.Services.Telegram.Abstractions;
using FinanceManager.Bot.Services.UserServices;
using FinanceManager.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace FinanceManager.Bot.Application;
public static class TelegramInjection
{
    public static IServiceCollection AddTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("telegram_bot_client").RemoveAllLoggers()
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var botConfiguration = sp.GetService<IOptionsSnapshot<AppSettings>>()?.Value;

                    ArgumentNullException.ThrowIfNull(botConfiguration);
                    TelegramBotClientOptions options = new(botConfiguration.BotToken);

                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<IReceiverService, ReceiverService>();
        services.AddScoped<IPollingService, PollingService>();
        services.AddScoped<IUserSessionManager, UserSessionManager>();
        services.AddSingleton<IUserSessionProvider, UserSessionProvider>();

        return services;
    }
}
