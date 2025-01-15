﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class SendCurrenciesStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IMessageManager _messageManager;

    public SendCurrenciesStateHandler(
        ICurrencyManager currencyManager, IMessageManager messageManager)
    {
        _currencyManager = currencyManager;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var currencies = await _currencyManager.GetAll(updateContext.CancellationToken);
        var inlineKeyboard = CreateInlineKeyboard(currencies);

        await _messageManager.SendInlineKeyboardMessage(updateContext, "Choose currency:", inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.ChooseCurrency);
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