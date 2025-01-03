using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class SetAccountBalanceSubStateHandler : ISubStateHandler
{
    private readonly IUpdateMessageProvider _updateMessageProvider;

    public SetAccountBalanceSubStateHandler(IUpdateMessageProvider updateMessageProvider)
    {
        _updateMessageProvider = updateMessageProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_updateMessageProvider.GetMessage(update, out var message))
            return UserSubState.SetAccountInitialBalance;

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await botClient.SendMessage(
                message.Chat, $"{Enums.Emoji.Error.GetSymbol()} " +
                $"The entered value is not a number. Try again.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserSubState.SetAccountInitialBalance;
        }
        var context = session.GetCreateAccountContext();
        context.InitialBalance = amount;
        session.ContextData = context;
        return UserSubState.Complete;
    }
}