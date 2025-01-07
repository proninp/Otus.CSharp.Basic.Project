using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class TransactionDefaultStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public TransactionDefaultStateHandler(IChatProvider chatProvider, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task<UserSubState> HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);
        var context = session.GetTransactionContext();

        await _messageSender.SendMessage(
            botClient, chat,
            $"Please enter the date (dd mm yyyy) of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}:",
            cancellationToken);

        return UserSubState.SetExpenseDate;
    }
}
