using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionInputDateStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public TransactionInputDateStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var inlineKeyboard = CreateInlineKeyboard(updateContext);
        var message =
            $"Please choose or enter the date of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}" +
             $"{Environment.NewLine}" +
             "You can enter date in <code>dd</code>, <code>dd.mm</code> or <code>dd.mm.yyyy</code> formats:";

        if (! await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext updateContext)
    {
        var dateFormat = "dd/MM/yyyy";
        var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var today = DateOnly.FromDateTime(DateTime.Today);

        var buttons = new List<InlineKeyboardButton>()
        {
            _messageManager.CreateInlineButton(updateContext, yesterday.ToString(dateFormat), "Yesterday"),
            _messageManager.CreateInlineButton(updateContext, yesterday.ToString(dateFormat), "Today"),
        };

        return new InlineKeyboardMarkup(buttons);
    }
}