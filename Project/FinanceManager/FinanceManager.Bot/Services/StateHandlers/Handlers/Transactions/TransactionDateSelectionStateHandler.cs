using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionDateSelectionStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;

    public TransactionDateSelectionStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var inlineKeyboard = CreateInlineKeyboard();
        var message =
            $"Please choose or enter the date of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}" +
             $"{Environment.NewLine}" +
             "You can enter date in <code>dd</code>, <code>dd.mm</code> or <code>dd.mm.yyyy</code> formats:";

        if (! await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.SetTransactionDate);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var dateFormat = "dd/MM/yyyy";
        var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var today = DateOnly.FromDateTime(DateTime.Today);

        var buttons = new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("Yesterday", yesterday.ToString(dateFormat)),
            InlineKeyboardButton.WithCallbackData("Today", today.ToString(dateFormat)),
        };

        return new InlineKeyboardMarkup(buttons);
    }
}