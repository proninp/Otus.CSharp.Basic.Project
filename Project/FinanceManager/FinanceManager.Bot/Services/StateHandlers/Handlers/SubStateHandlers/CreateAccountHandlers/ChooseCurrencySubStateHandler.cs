using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
public class ChooseCurrencySubStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public ChooseCurrencySubStateHandler(ICurrencyManager currencyManager,
        IUpdateCallbackQueryProvider updateCallbackQueryProvider,
        IChatProvider chatProvider,
        IMessageSenderManager messageSender)
    {
        _currencyManager = currencyManager;
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var previousState = WorkflowSubState.SendCurrencies;
        if (!_updateCallbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
        {
            session.Continue(previousState);
            return;
        }

        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var currencyId = callbackQuery.Data;
        if (string.IsNullOrEmpty(currencyId))
        {
            session.Continue(previousState);
            return;
        }

        var currency = await _currencyManager.GetById(new Guid(currencyId), cancellationToken);
        if (currency is null)
        {
            session.Continue(previousState);
            return;
        }

        var context = session.GetCreateAccountContext();
        context.Currency = currency;
        session.ContextData = context;

        await _messageSender.SendMessage(
            botClient, chat, "Enter a number to set the initial balance:", cancellationToken);

        session.Wait(WorkflowSubState.SetAccountInitialBalance);
    }
}