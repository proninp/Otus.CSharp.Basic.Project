using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class CreateAccountDefaultSubStateHandler : ISubStateHandler
{
    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            message.Chat, $"Please enter the account name.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        return UserSubState.ChooseAccountName;
    }
}
