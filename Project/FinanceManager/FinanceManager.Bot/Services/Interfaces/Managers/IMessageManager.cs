using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IMessageManager
{
    Task SendMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true);

    Task SendErrorMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = false);

    Task SendApproveMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = false);

    Task SendInlineKeyboardMessageAsync(BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard);

    Task<bool> EditLastMessageAsync(BotUpdateContext updateContext, string newMessageText, InlineKeyboardMarkup? inlineKeyboard = default);

    Task<bool> DeleteLastMessageAsync(BotUpdateContext updateContext);

    Task<bool> DeleteMessageAsync(BotUpdateContext updateContext, int messageId);

    InlineKeyboardButton CreateInlineButton(BotUpdateContext updateContext, string data, string message);

    InlineKeyboardButton CreateInlineButton(CallbackData data, string message);
}