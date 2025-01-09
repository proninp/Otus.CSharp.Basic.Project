using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class TransactionSendDateSelectionSubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public TransactionSendDateSelectionSubStateHandler(
        IChatProvider chatProvider, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);
        var context = session.GetTransactionContext();

        var inlineKeyboard = CreateInlineKeyboard();

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat,
             $"Please choose or enter the date of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}" +
             $"{Environment.NewLine}" +
             "You can enter date in <code>dd</code>, <code>dd.mm</code> or <code>dd.mm.yyyy</code> formats:",
            inlineKeyboard, cancellationToken);

        session.Wait(WorkflowSubState.SetTransactionDate);
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
