using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
public class ChooseCurrencySubStateHandler : ISubStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;
    private readonly IChatProvider _chatProvider;

    public ChooseCurrencySubStateHandler(ICurrencyManager currencyManager,
        IUpdateCallbackQueryProvider updateCallbackQueryProvider,
        IChatProvider chatProvider)
    {
        _currencyManager = currencyManager;
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
        _chatProvider = chatProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var currentSate = session.SubState;
        if (!_updateCallbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
            return currentSate;

        var currencyId = callbackQuery.Data;
        if (!string.IsNullOrEmpty(currencyId))
        {
            var currency = await _currencyManager.GetById(new Guid(currencyId), cancellationToken);
            if (currency is not null)
            {
                var context = session.GetCreateAccountContext();
                context.Currency = currency;
                session.ContextData = context;

                var chat = _chatProvider.GetChat(update);

                await botClient.SendMessage(
                    chat, "Enter a number to set the initial balance:",
                    parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());

                return UserSubState.SetAccountInitialBalance;
            }
        }
        return currentSate;
    }
}