using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IMessageSenderManager
{
    Task SendMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken);

    Task SendErrorMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken);

    Task SendApproveMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken);
    
    Task SendInlineKeyboardMessage(
        ITelegramBotClient botClient, Chat chat, string messageText,
        IReplyMarkup inlineKeyboard, CancellationToken cancellationToken);
}