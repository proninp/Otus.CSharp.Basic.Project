using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Factories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Application;
public static class StateHandlersInjection
{
    public static IServiceCollection AddStateHandlers(this IServiceCollection services)
    {
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
