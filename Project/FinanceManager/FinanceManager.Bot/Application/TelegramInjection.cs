using FinanceManager.Bot.Services;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Factories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
using FinanceManager.Bot.Services.Telegram;
using FinanceManager.Bot.Services.Telegram.Abstractions;
using FinanceManager.Bot.Services.Telegram.Providers;
using FinanceManager.Bot.Services.UserServices;
using FinanceManager.Bot.Services.Utils;
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
            .AddScoped<IPollingService, PollingService>()
            .AddScoped<IReceiverService, ReceiverService>()
            .AddScoped<UpdateHandler>()
            .AddScoped<IBotStateManager, BotStateManager>();

        services
            .AddSingleton<IUserSessionRegistry, UserSessionRegistry>()
            .AddScoped<IUserSessionManager, UserSessionManager>()
            .AddScoped<IUserSessionProvider, UserSessionProvider>();

        services
            .AddScoped<IUpdateValidator, UpdateValidator>()
            .AddScoped<IUpdateMessageProvider, UpdateMessageProvider>()
            .AddScoped<IUpdateCallbackQueryProvider, UpdateCallbackQueryProvider>()
            .AddScoped<IChatProvider, ChatProvider>()
            .AddScoped<IMessageManager, MessageService>()
            .AddScoped<ITransactionDateProvider, TransactionDateProvider>();

        services
            .AddScoped<IStateHandlerFactory, StateHandlerFactory>();

        services
            .AddScoped<DefaultStateHandler>()
            .AddScoped<CreateMenuStateHandler>()
            .AddScoped<SelectMenuStateHandler>()
            .AddScoped<RegisterExpenseStartStateHandler>()
            .AddScoped<RegisterIncomeStartStateHandler>()
            .AddScoped<HistoryStateHandler>()
            .AddScoped<SettingsStateHandler>();

        services
            .AddScoped<CreateAccountStartStateHandler>()
            .AddScoped<ChooseAccountNameStateHandler>()
            .AddScoped<SendCurrenciesStateHandler>()
            .AddScoped<ChooseCurrencyStateHandler>()
            .AddScoped<SetAccountBalanceStateHandler>()
            .AddScoped<CreateAccountEndStateHandler>();

        services
            .AddScoped<SendCategoriesStateHandler>()
            .AddScoped<ChooseCategoryStateHandler>()
            .AddScoped<TransactionDateSelectionStateHandler>()
            .AddScoped<TransactionSetDateStateHandler>()
            .AddScoped<TransactionSetAmountStateHandler>()
            .AddScoped<TransactionRegistrationStateHandler>();

        return services;
    }
}
