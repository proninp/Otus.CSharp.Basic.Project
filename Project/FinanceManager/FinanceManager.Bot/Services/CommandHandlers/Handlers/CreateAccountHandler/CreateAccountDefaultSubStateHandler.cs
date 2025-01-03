using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class CreateAccountDefaultSubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;

    public CreateAccountDefaultSubStateHandler(IChatProvider chatProvider)
    {
        _chatProvider = chatProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);
        await botClient.SendMessage(
            chat, $"Please enter the account name:",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        return UserSubState.ChooseAccountName;
    }
}
