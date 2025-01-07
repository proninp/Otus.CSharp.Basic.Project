using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class MenuStateHandler : IStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageSenderManager _messageSender;

    public MenuStateHandler(
        IChatProvider chatProvider, IUpdateCallbackQueryProvider callbackQueryProvider, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _callbackQueryProvider = callbackQueryProvider;
        _messageSender = messageSender;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (userSession.SubState)
        {
            case UserSubState.Default:
                userSession.SubState = await HandleDefaultSubState(userSession, botClient, update, cancellationToken);
                break;

            case UserSubState.SelectMenu:
                var nextState = HandleMenuSelection(userSession, botClient, update, cancellationToken);
                if (nextState != userSession.UserState)
                {
                    userSession.ResetState();
                    return nextState;
                }
                break;
        }

        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<UserSubState> HandleDefaultSubState(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);

        var inlineKeyboard = CreateInlineKeyboard();

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat, "Choose an action:", inlineKeyboard, cancellationToken);

        return UserSubState.SelectMenu;
    }

    private UserState HandleMenuSelection(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery) || callbackQuery.Data is null)
            return session.UserState;

        var stateMapping = new Dictionary<string, UserState>
        {
            { nameof(RegisterExpenseStateHandler), UserState.AddExpense },
            { nameof(RegisterIncomeStateHandler), UserState.AddIncome },
            { nameof(HistoryStateHandler), UserState.History },
            { nameof(SettingsStateHandler), UserState.Settings }
        };

        if (stateMapping.TryGetValue(callbackQuery.Data, out var newState))
            return newState;

        return session.UserState;
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Enums.Emoji.Expense.GetSymbol()} Expense", $"{nameof(RegisterExpenseStateHandler)}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Enums.Emoji.Income.GetSymbol()} Income", $"{nameof(RegisterIncomeStateHandler)}"),
            ],
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Enums.Emoji.History.GetSymbol()} History", $"{nameof(HistoryStateHandler)}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Enums.Emoji.Settings.GetSymbol()} Settings", $"{nameof(SettingsStateHandler)}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}