using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class SendCurrenciesStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public SendCurrenciesStateHandler(
        ICurrencyManager currencyManager, IChatProvider chatProvider, IMessageSenderManager messageSenderManager)
    {
        _currencyManager = currencyManager;
        _chatProvider = chatProvider;
        _messageSender = messageSenderManager;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var currencies = await _currencyManager.GetAll(cancellationToken);
        var inlineKeyboard = CreateInlineKeyboard(currencies);

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat, "Choose currency:", inlineKeyboard, cancellationToken);

        session.Wait(WorkflowState.ChooseCurrency);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(CurrencyDto[] currencies)
    {
        var buttons = currencies
            .Select(c => InlineKeyboardButton.WithCallbackData($"{c.Emoji} {c.CurrencyCode}", c.Id.ToString()))
            .ToList();

        var keyboardButtons = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToArray();

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}