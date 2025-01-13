using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram;
public class MessageSenderService : IMessageSenderManager
{
    public async Task SendMessage(BotUpdateContext updateContext, string messageText)
    {
        await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: updateContext.CancellationToken);
    }

    public async Task SendErrorMessage(BotUpdateContext updateContext, string messageText)
    {
        messageText = $"{Enums.Emoji.Error.GetSymbol()} " + messageText;
        await SendMessage(updateContext, messageText);
    }

    public async Task SendApproveMessage(BotUpdateContext updateContext, string messageText)
    {
        messageText = $"{Enums.Emoji.Success.GetSymbol()} " + messageText;
        await SendMessage(updateContext, messageText);
    }

    public async Task SendInlineKeyboardMessage(
        BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard)
    {
        await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: updateContext.CancellationToken);
    }
}