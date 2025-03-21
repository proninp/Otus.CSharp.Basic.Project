﻿using FinanceManager.Bot.Services;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Providers;
using FinanceManager.Bot.Services.StateHandlers.Validators;
using FinanceManager.Bot.Services.Telegram;
using FinanceManager.Bot.Services.Telegram.Abstractions;
using FinanceManager.Bot.Services.Telegram.Handlers;
using FinanceManager.Bot.Services.Telegram.Providers;
using FinanceManager.Bot.Services.Telegram.Validators;
using FinanceManager.Bot.Services.UserServices;
using FinanceManager.Bot.Services.UserServices.SessianStateManager;
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
            .AddSingleton<ISessionRegistry, SessionRegistry>()
            .AddSingleton<ISessionStateRegistry, SessionStateRegistry>()
            .AddScoped<ISessionManager, SessionManager>()
            .AddScoped<ISessionProvider, SessionProvider>()
            .AddScoped<ISessionStateManager, SessionStateManager>();

        services
            .AddScoped<IUpdateValidator, UpdateValidator>()
            .AddScoped<IUpdateMessageProvider, UpdateMessageProvider>()
            .AddScoped<ISessionConsistencyValidator, SessionConsistencyValidator>()
            .AddScoped<ICallbackDataProvider, CallbackDataProvider>()
            .AddScoped<IChatProvider, ChatProvider>()
            .AddScoped<IMessageManager, MessageManager>()
            .AddScoped<ITransactionDateProvider, TransactionDateProvider>()
            .AddScoped<IDecimalNumberProvider, DecimalNumberProvider>()
            .AddScoped<IHistoryMessageTextProvider, HistoryMessageTextProvider>()
            .AddScoped<IHistoryContextProvider, HistoryContextProvider>()
            .AddScoped<IHistoryInlineKeyBoardProvider, HistoryInlineKeyBoardProvider>()
            .AddScoped<IAccountInfoProvider, AccountInfoProvider>()
            .AddScoped<IMenuCallbackHandler, MenuCallbackHandler>()
            .AddScoped<ITextSanitizer, TextSanitizer>()
            .AddScoped<IEmojiTextValidator, EmojiTextValidator>()
            .AddScoped<ICategoryTitleValidator, CategoryTitleValidator>();

        services.AddStateHandlers();

        return services;
    }
}