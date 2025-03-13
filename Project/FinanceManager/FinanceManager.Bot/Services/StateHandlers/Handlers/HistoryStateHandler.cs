using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers;
public sealed class HistoryStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly IMessageManager _messageManager;
    private readonly IHistoryMessageTextProvider _historyMessageTextProvider;
    private readonly IHistoryContextProvider _contextProvider;
    private readonly IHistoryInlineKeyBoardProvider _inlineKeyboardProvider;
    private readonly ITransactionManager _transactionManager;
    private readonly ISessionStateManager _sessionStateManager;

    public HistoryStateHandler(
        ICallbackDataProvider callbackQueryProvider,
        IMessageManager messageManager,
        IHistoryMessageTextProvider historyMessageTextProvider,
        IHistoryInlineKeyBoardProvider inlineKeyboardProvider,
        IHistoryContextProvider contextProvider,
        ITransactionManager transactionManager,
        ISessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackQueryProvider;
        _messageManager = messageManager;
        _historyMessageTextProvider = historyMessageTextProvider;
        _inlineKeyboardProvider = inlineKeyboardProvider;
        _contextProvider = contextProvider;
        _transactionManager = transactionManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true);
        if (callbackData is null)
            return await _sessionStateManager.ToMainMenu(updateContext.Session);

        var context = await _contextProvider.GetHistoryContex(updateContext);
        if (context is null)
            return false;

        if (callbackData.Data == NavigationCommand.Newer.GetCallbackData())
        {
            context.PageIndex--;
        }
        else if (callbackData.Data == NavigationCommand.Older.GetCallbackData())
        {
            context.PageIndex++;
        }

        var inlineKeyboard = _inlineKeyboardProvider.GetKeyboard(updateContext);

        var transactions = await _transactionManager.GetAsync(
            updateContext.Session.Id, updateContext.CancellationToken, context.PageIndex, context.PageSize);

        var incomes = transactions.Where(t => t.Amount > 0);
        var expenses = transactions.Where(t => t.Amount < 0);

        var messageText = _historyMessageTextProvider.GetMessgaText(incomes, expenses);

        if (!await _messageManager.EditLastMessageAsync(updateContext, messageText, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessageAsync(updateContext, messageText, inlineKeyboard);

        return false;
    }
}