﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class SendCurrenciesStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public SendCurrenciesStateHandler(
        ICurrencyManager currencyManager, IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _currencyManager = currencyManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var currencies = await _currencyManager.GetAllAsync(updateContext.CancellationToken);
        var inlineKeyboard = CreateInlineKeyboard(updateContext, currencies);

        await _messageManager.SendInlineKeyboardMessageAsync(updateContext, "Choose currency:", inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context, CurrencyDto[] currencies)
    {
        var buttons = currencies
            .Select(c => _messageManager
                .CreateInlineButton(context, c.Id.ToString(), $"{c.Emoji} {c.CurrencyCode}"))
            .ToList();

        var keyboardButtons = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToArray();

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}