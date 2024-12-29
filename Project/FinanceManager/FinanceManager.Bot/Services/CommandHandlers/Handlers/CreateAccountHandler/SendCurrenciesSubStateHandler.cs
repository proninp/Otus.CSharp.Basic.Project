using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class SendCurrenciesSubStateHandler : ISubStateHandler
{
    private readonly ICurrencyManager _currencyManager;

    public SendCurrenciesSubStateHandler(ICurrencyManager currencyManager)
    {
        _currencyManager = currencyManager;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var currencies = await _currencyManager.GetAll(cancellationToken);

        var inlineKeyboard = CreateInlineKeyboard(currencies);

        await botClient.SendMessage(
               message.Chat, "Choose currency",
           parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
        return UserSubState.ChooseCurrency;
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(CurrencyDto[] currencies)
    {
        var buttons = currencies
            .Select(c => InlineKeyboardButton.WithCallbackData($"{c.Emoji} {c.CurrencyCode}", c.Id.ToString()))
            .ToList();

        var keyboardRows = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToArray();

        return new InlineKeyboardMarkup(keyboardRows);
    }
}