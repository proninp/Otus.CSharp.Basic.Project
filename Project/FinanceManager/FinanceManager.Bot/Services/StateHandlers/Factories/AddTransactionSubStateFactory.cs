using FinanceManager.Bot.Enums;
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

    public ISubStateHandler GetSubStateHandler(WorkflowSubState userSubState) => userSubState switch
    {
        WorkflowSubState.Default => _serviceProvider.GetRequiredService<SendCategoriesStateHandler>(),
        WorkflowSubState.ChooseTransactionCategory => _serviceProvider.GetRequiredService<ChooseCategoryStateHandler>(),
        WorkflowSubState.SetTransactionDate => _serviceProvider.GetRequiredService<TransactionSetDateStateHandler>(),
        WorkflowSubState.SetTransactionAmount => _serviceProvider.GetRequiredService<TransactionSetAmountStateHandler>(),
        WorkflowSubState.RegisterTransaction => _serviceProvider.GetRequiredService<TransactionRegistrationHandler>(),
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState}")
    };
}