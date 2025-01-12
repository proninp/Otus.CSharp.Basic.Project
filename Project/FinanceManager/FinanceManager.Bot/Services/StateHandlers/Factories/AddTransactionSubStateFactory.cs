using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class AddTransactionSubStateFactory : ISubStateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AddTransactionSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetSubStateHandler(UserState userState) => userState.SubState switch
    {
        WorkflowSubState.Default =>
            _serviceProvider.GetRequiredService<SendCategoriesSubStateHandler>(),
        WorkflowSubState.ChooseTransactionCategory =>
            _serviceProvider.GetRequiredService<ChooseCategorySubStateHandler>(),
        WorkflowSubState.SendTransactionDateSelection =>
            _serviceProvider.GetRequiredService<TransactionSendDateSelectionSubStateHandler>(),
        WorkflowSubState.SetTransactionDate =>
            _serviceProvider.GetRequiredService<TransactionSetDateSubStateHandler>(),
        WorkflowSubState.SetTransactionAmount =>
            _serviceProvider.GetRequiredService<TransactionSetAmountSubStateHandler>(),
        WorkflowSubState.RegisterTransaction =>
            _serviceProvider.GetRequiredService<TransactionRegistrationSubStateHandler>(),
        _ => throw new SubStateHandlerNotFoundException(userState.State, userState.SubState)
    };
}