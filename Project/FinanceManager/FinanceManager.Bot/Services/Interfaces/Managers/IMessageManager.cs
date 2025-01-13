using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IMessageManager
{
    Task SendMessage(BotUpdateContext updateContext, string messageText);

    Task SendErrorMessage(BotUpdateContext updateContext, string messageText);

    Task SendApproveMessage(BotUpdateContext updateContext, string messageText);

    Task SendInlineKeyboardMessage(BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard);

    Task<bool> EditLastMessage(BotUpdateContext updateContext, string newMessageText);

    Task<bool> DeleteLastMessage(BotUpdateContext updateContext);
}