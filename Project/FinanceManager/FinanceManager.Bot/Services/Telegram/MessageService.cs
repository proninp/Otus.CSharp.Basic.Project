using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram;
public class MessageService : IMessageManager
{
    public async Task SendMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true)
    {
        var message =
            await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: updateContext.CancellationToken);
        if (isSaveMessage)
            updateContext.Session.LastMessage = message;
    }

    public async Task SendErrorMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true)
    {
        messageText = $"{Enums.Emoji.Error.GetSymbol()} " + messageText;
        await SendMessage(updateContext, messageText, isSaveMessage);
    }

    public async Task SendApproveMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true)
    {
        messageText = $"{Enums.Emoji.Success.GetSymbol()} " + messageText;
        await SendMessage(updateContext, messageText, isSaveMessage);
    }

    public async Task SendInlineKeyboardMessage(
        BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard)
    {
        updateContext.Session.LastMessage =
            await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: updateContext.CancellationToken);
    }

    public async Task<bool> EditLastMessage(BotUpdateContext updateContext, string newMessageText, InlineKeyboardMarkup? inlineKeyboard = default)
    {
        if (updateContext.Session.LastMessage == default)
            return false;

        updateContext.Session.LastMessage = await updateContext.BotClient.EditMessageText(
            chatId: updateContext.Chat.Id,
            messageId: updateContext.Session.LastMessage.MessageId,
            text: newMessageText,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboard,
            cancellationToken: updateContext.CancellationToken
        );

        return true;
    }

    public async Task<bool> DeleteLastMessage(BotUpdateContext updateContext)
    {
        if (updateContext.Session.LastMessage is null)
            return false;

        await updateContext.BotClient.DeleteMessage(
            chatId: updateContext.Chat,
            messageId: updateContext.Session.LastMessage.Id,
            cancellationToken: updateContext.CancellationToken
            );

        updateContext.Session.LastMessage = null;

        return true;
    }
}