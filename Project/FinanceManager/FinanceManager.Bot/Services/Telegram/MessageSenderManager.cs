using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram;
public class MessageSenderManager : IMessageSenderManager
{
    public async Task SendMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    public async Task SendErrorMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken)
    {
        messageText = $"{Enums.Emoji.Error.GetSymbol()} " + messageText;
        await SendMessage(botClient, chat, messageText, cancellationToken);
    }

    public async Task SendApproveMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken)
    {
        messageText = $"{Enums.Emoji.Success.GetSymbol()} " + messageText;
        await SendMessage(botClient, chat, messageText, cancellationToken);
    }

    public async Task SendInlineKeyboardMessage(
        ITelegramBotClient botClient, Chat chat, string messageText,
        IReplyMarkup inlineKeyboard, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
    }
}