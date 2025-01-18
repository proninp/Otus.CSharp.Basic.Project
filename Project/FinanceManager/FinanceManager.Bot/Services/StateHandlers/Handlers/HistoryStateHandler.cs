using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class HistoryStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageManager _messageManager;
    private readonly IHistoryMessageTextProvider _historyMessageTextProvider;
    private readonly IHistoryContextProvider _contextProvider;
    private readonly IHistoryInlineKeyBoardProvider _inlineKeyboardProvider;
    private readonly ITransactionManager _transactionManager;

    public HistoryStateHandler(
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageManager messageManager,
        IHistoryMessageTextProvider historyMessageTextProvider,
        IHistoryInlineKeyBoardProvider inlineKeyboardProvider,
        IHistoryContextProvider contextProvider,
        ITransactionManager transactionManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _messageManager = messageManager;
        _historyMessageTextProvider = historyMessageTextProvider;
        _inlineKeyboardProvider = inlineKeyboardProvider;
        _contextProvider = contextProvider;
        _transactionManager = transactionManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_callbackQueryProvider.GetCallbackQuery(updateContext.Update, out var callbackQuery))
        {
            updateContext.Session.Reset();
            return;
        }

        var context = await _contextProvider.GetHistoryContex(updateContext);
        if (context is null)
            return;

        var data = callbackQuery.Data ?? string.Empty;

        if (data == HistoryCommand.Next.ToString())
        {
            context.PageIndex++;
        }
        else if (data == HistoryCommand.Previous.ToString())
        {
            context.PageIndex--;
        }
        else if (data == HistoryCommand.Memu.ToString())
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