using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionDateSelectionStateHandler : IStateHandler
{
    private readonly IMessageManager _messageSender;

    public TransactionDateSelectionStateHandler(IMessageManager messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var inlineKeyboard = CreateInlineKeyboard();

        await _messageSender.SendInlineKeyboardMessage(updateContext,
             $"Please choose or enter the date of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}" +
             $"{Environment.NewLine}" +
             "You can enter date in <code>dd</code>, <code>dd.mm</code> or <code>dd.mm.yyyy</code> formats:",
            inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.SetTransactionDate);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var today = DateOnly.FromDateTime(DateTime.Today);

        var buttons = new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData("Yesterday", yesterday.ToString()),
            InlineKeyboardButton.WithCallbackData("Today", today.ToString()),
        };

        return new InlineKeyboardMarkup(buttons);
    }
}