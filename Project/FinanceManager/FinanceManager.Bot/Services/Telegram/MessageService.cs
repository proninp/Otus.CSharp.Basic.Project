using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram;
public class MessageService : IMessageManager
{
    public async Task SendMessage(BotUpdateContext updateContext, string messageText)
    {
        updateContext.LastMessage =
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
        updateContext.LastMessage =
            await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: updateContext.CancellationToken);
    }

    public async Task<bool> EditLastMessage(BotUpdateContext updateContext, string newMessageText)
    {
        if (updateContext.LastMessage is null)
            return false;

        updateContext.LastMessage = await updateContext.BotClient.EditMessageText(
            chatId: updateContext.Chat.Id,
            messageId: updateContext.LastMessage.MessageId,
            text: newMessageText,
            cancellationToken: updateContext.CancellationToken
        );

        return true;
    }

    public async Task<bool> DeleteLastMessage(BotUpdateContext updateContext)
    {
        if (updateContext.LastMessage is null)
            return false;

        await updateContext.BotClient.DeleteMessage(
            chatId: updateContext.Chat,
            messageId: updateContext.LastMessage.Id,
            cancellationToken: updateContext.CancellationToken
            );

        updateContext.LastMessage = null;

        return true;
    }
}