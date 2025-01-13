using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IMessageSenderManager
{
    Task SendMessage(BotUpdateContext updateContext, string messageText);

    Task SendErrorMessage(BotUpdateContext updateContext, string messageText);

    Task SendApproveMessage(BotUpdateContext updateContext, string messageText);

    Task SendInlineKeyboardMessage(BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard);
}