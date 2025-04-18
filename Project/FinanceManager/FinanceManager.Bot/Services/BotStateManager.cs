﻿using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services;
public sealed class BotStateManager : IBotStateManager
{
    private readonly ISessionProvider _userSessionProvider;
    private readonly IStateHandlerFactory _stateHandlerFactory;
    private readonly IChatProvider _chatProvider;
    private readonly ISessionConsistencyValidator _consistencyValidator;
    private readonly IMenuCallbackHandler _menuCallbackHandler;

    public BotStateManager(
        ISessionProvider userSessionProvider,
        IStateHandlerFactory stateHandlerFactory,
        IChatProvider chatProvider,
        ISessionConsistencyValidator consistencyValidator,
        IMenuCallbackHandler menuCallbackHandler)
    {
        _userSessionProvider = userSessionProvider;
        _stateHandlerFactory = stateHandlerFactory;
        _chatProvider = chatProvider;
        _consistencyValidator = consistencyValidator;
        _menuCallbackHandler = menuCallbackHandler;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient, Update update, User? user, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSessionAsync(user, cancellationToken);
        
        if (!_chatProvider.GetChat(session.Id, update, out var chat))
            return;

        var botContext = new BotUpdateContext(session, botClient, update, chat, cancellationToken);

        if (await _consistencyValidator.ValidateCallbackConsistencyAsync(botContext))
            await _menuCallbackHandler.HandleMenuCallbackAsync(botContext);

        bool isContinue;
        do
        {
            var handler = _stateHandlerFactory.GetHandler(session.State);
            isContinue = await handler.HandleAsync(botContext);
        } while (isContinue);
    }
}