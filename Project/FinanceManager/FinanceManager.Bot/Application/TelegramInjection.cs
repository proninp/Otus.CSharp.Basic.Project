using FinanceManager.Bot.Services;
using FinanceManager.Bot.Services.CommandHandlers;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
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

        services
            .AddSingleton<IUserSessionProvider, UserSessionProvider>()
            .AddScoped<UpdateHandler>()
            .AddScoped<IReceiverService, ReceiverService>()
            .AddScoped<IPollingService, PollingService>()
            .AddScoped<IUserSessionManager, UserSessionManager>()
            .AddScoped<IBotStateManager, BotStateManager>()
            .AddScoped<IStateHandlerFactory, StateHandlerFactory>()
            .AddScoped<ChooseCurrencyStateHandler>()
            .AddScoped<CreateAccountStateHandler>()
            .AddScoped<RegisterExpenseStateHandler>()
            .AddScoped<RegisterIncomeStateHandler>()
            .AddScoped<SetInitialBalanceStateHandler>()
            .AddScoped<StartStateHandler>()
            .AddScoped<DefaultStateHandler>();

        return services;
    }
}
