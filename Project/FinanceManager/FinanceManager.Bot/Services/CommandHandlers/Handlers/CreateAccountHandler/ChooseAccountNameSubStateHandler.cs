﻿using FinanceManager.Application.Services.Interfaces.Managers;
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
public class ChooseAccountNameSubStateHandler : ISubStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;

    public ChooseAccountNameSubStateHandler(IAccountManager accountManager, IUpdateMessageProvider messageProvider)
    {
        _accountManager = accountManager;
        _messageProvider = messageProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
            return UserSubState.ChooseAccountName;

        var accountTitle = message.Text;
        if (string.IsNullOrWhiteSpace(accountTitle) || accountTitle.Length == 0)
        {
            await botClient.SendMessage(
                message.Chat, $"{Enums.Emoji.Error.GetSymbol()} " +
                $"The account name must contain at least one non-whitespace character.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserSubState.ChooseAccountName;
        }
        if (!char.IsLetterOrDigit(accountTitle[0]))
        {
            await botClient.SendMessage(
                message.Chat, $"{Enums.Emoji.Error.GetSymbol()} " +
                $"The account name must start with a number or letter. Enter a different account name.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserSubState.ChooseAccountName;
        }
        var existingAccount = await _accountManager.GetByName(session.Id, accountTitle, false, cancellationToken);
        if (existingAccount is not null)
        {
            await botClient.SendMessage(
                message.Chat, $"{Enums.Emoji.Error.GetSymbol()} " +
                $"An account with that name already exists. Enter a different name.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserSubState.ChooseAccountName;
        }
        var context = new CreateAccountContext { AccountName = accountTitle };
        session.ContextData = context;
        return UserSubState.SendCurrencies;
    }
}
