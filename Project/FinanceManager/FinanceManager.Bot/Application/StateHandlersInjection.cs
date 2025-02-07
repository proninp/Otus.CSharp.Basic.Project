using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Factories;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageAccounts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageTransactions;
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
            .AddScoped<CreateSettingsMenuStateHandler>()
            .AddScoped<SelectSettingsMenuStateHandler>();

        services
            .AddScoped<CreateAccountStartStateHandler>()
            .AddScoped<ChooseAccountNameStateHandler>()
            .AddScoped<SendCurrenciesStateHandler>()
            .AddScoped<ChooseCurrencyStateHandler>()
            .AddScoped<SendInputAccountBalanceStateHandler>()
            .AddScoped<SetAccountBalanceStateHandler>()
            .AddScoped<CreateAccountEndStateHandler>();

        services
            .AddScoped<SendCategoriesStateHandler>()
            .AddScoped<ChooseCategoryStateHandler>()
            .AddScoped<TransactionInputDateStateHandler>()
            .AddScoped<TransactionSetDateStateHandler>()
            .AddScoped<TransactionIputAmountStateHandler>()
            .AddScoped<TransactionSetAmountStateHandler>()
            .AddScoped<TransactionRegistrationStateHandler>();

        services
            .AddScoped<ManageCategoriesStateHandler>()
            .AddScoped<ManageTransactionsStateHandler>()
            .AddScoped<ManageAccountsStateHandler>();

        return services;
    }
}
