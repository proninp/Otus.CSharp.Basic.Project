using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class TransactionIputAmountStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public TransactionIputAmountStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var emoji = context.TransactionType switch
        {
            TransactionType.Expense => Emoji.ExpenseAmount.GetSymbol(),
            TransactionType.Income => Emoji.IncomeAmount.GetSymbol(),
            _ => string.Empty
        };

        await _messageManager.SendInlineKeyboardMessageAsync(
            updateContext,
            $"Please enter {context.TransactionTypeDescription} {emoji} amount:",
            CreateInlineKeyboard(updateContext));

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext updateContext) =>
        new InlineKeyboardMarkup(_menuCallbackProvider.GetMenuButton(updateContext));
}
