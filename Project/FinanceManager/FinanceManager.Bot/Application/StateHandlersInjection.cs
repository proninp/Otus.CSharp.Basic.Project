using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Factories;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageAccounts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename;
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
            .AddScoped<SelectSettingsMenuStateHandler>()
            .AddScoped<CreateManageCategoriesMenuStateHandler>()
            .AddScoped<SelectManageCategoriesMenuStateHandler>();

        services
            .AddScoped<CreateAccountStartStateHandler>()
            .AddScoped<ChooseAccountNameStateHandler>()
            .AddScoped<SendCurrenciesStateHandler>()
            .AddScoped<ChooseCurrencyStateHandler>()
            .AddScoped<SendInputAccountBalanceStateHandler>()
            .AddScoped<SetAccountBalanceStateHandler>()
            .AddScoped<CreateAccountComplitionStateHandler>();

        services
            .AddScoped<TransactionSendCategoriesStateHandler>()
            .AddScoped<TransactionChooseCategoryStateHandler>()
            .AddScoped<TransactionInputDateStateHandler>()
            .AddScoped<TransactionSetDateStateHandler>()
            .AddScoped<TransactionIputAmountStateHandler>()
            .AddScoped<TransactionSetAmountStateHandler>()
            .AddScoped<TransactionComplitionStateHandler>();

        services
            .AddScoped<CreateManageCategoriesMenuStateHandler>()
            .AddScoped<SelectManageCategoriesMenuStateHandler>()
            .AddScoped<ManageTransactionsStateHandler>()
            .AddScoped<ManageAccountsStateHandler>();

        services
            .AddScoped<CreateCategoryStartStateHandler>()
            .AddScoped<CreateCategoryChooseTypeStateHandler>()
            .AddScoped<CreateCategoryInputTitleStateHandler>()
            .AddScoped<CreateCategorySetTitleStateHandler>()
            .AddScoped<CreateCategoryInputEmojiStateHandler>()
            .AddScoped<CreateCategorySetEmojiStateHandler>()
            .AddScoped<CreateCategoryComplitionStateHandler>();

        services
            .AddScoped<DeleteCategoryStartStateHandler>()
            .AddScoped<DeleteCategoryChooseTypeStateHandler>()
            .AddScoped<DeleteCategorySendCategoriesStateHandler>()
            .AddScoped<DeleteCategoryChooseStateHandler>()
            .AddScoped<DeleteCategorySendConfirmStateHadler>()
            .AddScoped<DeleteCategoryChooseConfirmStateHandler>()
            .AddScoped<DeleteCategoryComplitionStateHandler>();

        services
            .AddScoped<DeleteCategoryStartStateHandler>()
            .AddScoped<RenameCategoryUDStateHandler>();

        return services;
    }
}
