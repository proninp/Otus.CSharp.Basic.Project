using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class HistoryStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly IMessageManager _messageManager;
    private readonly IHistoryMessageTextProvider _historyMessageTextProvider;
    private readonly IHistoryContextProvider _contextProvider;
    private readonly IHistoryInlineKeyBoardProvider _inlineKeyboardProvider;
    private readonly ITransactionManager _transactionManager;

    public HistoryStateHandler(
        ICallbackDataProvider callbackQueryProvider,
        ICallbackDataValidator callbackDataValidator,
        ICallbackDataValidator dataValidator,
        IMessageManager messageManager,
        IHistoryMessageTextProvider historyMessageTextProvider,
        IHistoryInlineKeyBoardProvider inlineKeyboardProvider,
        IHistoryContextProvider contextProvider,
        ITransactionManager transactionManager)
    {
        _callbackDataProvider = callbackQueryProvider;
        _callbackDataValidator = callbackDataValidator;
        _messageManager = messageManager;
        _historyMessageTextProvider = historyMessageTextProvider;
        _inlineKeyboardProvider = inlineKeyboardProvider;
        _contextProvider = contextProvider;
        _transactionManager = transactionManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true, WorkflowState.CreateMenu);
        if (callbackData is null)
            return;

        if (!await _callbackDataValidator.Validate(updateContext, callbackData))
        {
            updateContext.Session.Wait();
            return;
        }

        var context = await _contextProvider.GetHistoryContex(updateContext);
        if (context is null)
            return;

        if (callbackData.Data == HistoryCommand.Newer.ToString())
        {
            context.PageIndex--;
        }
        else if (callbackData.Data == HistoryCommand.Older.ToString())
        {
            context.PageIndex++;
        }
        else if (callbackData.Data == HistoryCommand.Memu.ToString())
        {
            await _messageManager.DeleteLastMessage(updateContext);
            updateContext.Session.Continue(WorkflowState.CreateMenu);
            return;
        }

        var inlineKeyboard = _inlineKeyboardProvider.GetKeyboard(updateContext);

        var transactions = await _transactionManager.Get(
            updateContext.Session.Id, updateContext.CancellationToken, context.PageIndex, context.PageSize);

        var incomes = transactions.Where(t => t.Amount > 0);
        var expenses = transactions.Where(t => t.Amount < 0);

        var messageText = _historyMessageTextProvider.GetMessgaText(incomes, expenses);

        if (!await _messageManager.EditLastMessage(updateContext, messageText, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, messageText, inlineKeyboard);

        updateContext.Session.Wait();
    }
}