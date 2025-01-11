using FinanceManager.Bot.Services;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Factories;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
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
            .AddScoped<IMessageSenderManager, MessageSenderService>()
            .AddScoped<ITransactionDateProvider, TransactionDateProvider>();

        services
            .AddScoped<IStateHandlerFactory, StateHandlerFactory>()
            .AddScoped<ISubStateFactoryProvider, SubStateFactoryProvider>()
            .AddScoped<CreateAccountSubStateFactory>()
            .AddScoped<AddTransactionSubStateFactory>();
            
        services
            .AddScoped<DefaultStateHandler>()
            .AddScoped<MenuStateHandler>()
            .AddScoped<StateHandler>()
            .AddScoped<RegisterExpenseStateHandler>()
            .AddScoped<RegisterIncomeStateHandler>()
            .AddScoped<HistoryStateHandler>()
            .AddScoped<SettingsStateHandler>();

        services
            .AddScoped<CreateAccountDefaultSubStateHandler>()
            .AddScoped<ChooseAccountNameSubStateHandler>()
            .AddScoped<SendCurrenciesSubStateHandler>()
            .AddScoped<ChooseCurrencySubStateHandler>()
            .AddScoped<SetAccountBalanceSubStateHandler>()
            .AddScoped<CreateAccountCompleteSubStateHandler>();

        services
            .AddScoped<SendCategoriesSubStateHandler>()
            .AddScoped<ChooseCategorySubStateHandler>()
            .AddScoped<TransactionSendDateSelectionSubStateHandler>()
            .AddScoped<TransactionSetDateSubStateHandler>()
            .AddScoped<TransactionSetAmountSubStateHandler>()
            .AddScoped<TransactionRegistrationSubStateHandler>();

        return services;
    }
}
