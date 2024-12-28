using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class SetAccountBalanceSubStateHandler : ISubStateHandler
{
    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await botClient.SendMessage(
                message.Chat, $"{Enums.Emoji.Error.GetSymbol()} " +
                $"Enter a number to set the initial balance.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserSubState.SetAccountInitialBalance;
        }
        var context = session.GetCreateAccountContext();
        context.InitialBalance = amount;
        session.ContextData = context;
        return UserSubState.Complete;
    }
}