using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class CompleteSubStateHandler : IStateHandler
{
    private protected readonly IChatProvider _chatProvider;
    private protected readonly IMessageSenderManager _messageSender;

    protected CompleteSubStateHandler(IChatProvider chatProvider, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            _chatProvider.GetChat(update, out var chat);
            ArgumentNullException.ThrowIfNull(chat, nameof(chat));

            await HandleCompleteAsync(session, botClient, update, chat, cancellationToken);
        }
        catch (Exception)
        {
            if (_chatProvider.GetChat(update, out var chat))
            {
                await _messageSender.SendErrorMessage(botClient, chat,
                    "An error occurred while performing the action. Please try again later.",
                    cancellationToken);
            }
            throw;
        }
        finally
        {
            session.Reset();
        }
    }

    private protected abstract Task HandleCompleteAsync(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken);

}
