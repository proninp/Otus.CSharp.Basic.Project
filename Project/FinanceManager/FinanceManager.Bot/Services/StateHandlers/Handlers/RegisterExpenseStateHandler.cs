using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterExpenseStateHandler : IStateHandler
{
    private readonly ISubStateFactoryProvider _subStateFactoryProvider;

    public RegisterExpenseStateHandler(ISubStateFactoryProvider subStateFactoryProvider)
    {
        _subStateFactoryProvider = subStateFactoryProvider;
    }

    public async Task HandleStateAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        AddExpenseContext(session);

        var subStateHandlerFactory = _subStateFactoryProvider.GetSubStateFactory(session.State);
        var subStateHandler = subStateHandlerFactory.GetSubStateHandler(session.SubState);

        await subStateHandler.HandleAsync(session, botClient, update, cancellationToken);

        if (session.SubState == WorkflowSubState.Complete)
            session.ResetState();
    }

    public Task RollBackAsync(UserSession session, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void AddExpenseContext(UserSession session)
    {
        if (session.ContextData is null)
        {
            session.SetData(new TransactionContext { TransactionType = TransactionType.Expense });
        }
    }
}
