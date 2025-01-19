using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IMessageManager
{
    Task SendMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true);

    Task SendErrorMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = false);

    Task SendApproveMessage(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true);

    Task SendInlineKeyboardMessage(BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard);

    Task<bool> EditLastMessage(BotUpdateContext updateContext, string newMessageText, InlineKeyboardMarkup? inlineKeyboard = default);

    Task<bool> DeleteLastMessage(BotUpdateContext updateContext);

    InlineKeyboardButton CreateInlineButton(BotUpdateContext updateContext, string data, string message);

    InlineKeyboardButton CreateInlineButton(CallbackData data, string message);
}