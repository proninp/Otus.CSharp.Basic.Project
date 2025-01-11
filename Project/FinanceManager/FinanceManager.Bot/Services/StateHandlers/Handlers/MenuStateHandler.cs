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

    public async Task HandleStateAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (session.SubState)
        {
            case WorkflowSubState.Default:
                await HandleDefaultSubState(session, botClient, update, cancellationToken);
                break;

            case WorkflowSubState.SelectMenu:
                HandleMenuSelection(session, botClient, update, cancellationToken);
                break;
        }
    }

    public Task RollBackAsync(UserSession session, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleDefaultSubState(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var inlineKeyboard = CreateInlineKeyboard();

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat, "Choose an action:", inlineKeyboard, cancellationToken);

        session.Wait(WorkflowSubState.SelectMenu);
    }

    private void HandleMenuSelection(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery) || callbackQuery.Data is null)
        {
            session.ResetState();
            return;
        }

        var stateMapping = new Dictionary<string, WorkflowState>
        {
            { nameof(RegisterExpenseStateHandler), WorkflowState.AddExpense },
            { nameof(RegisterIncomeStateHandler), WorkflowState.AddIncome },
            { nameof(HistoryStateHandler), WorkflowState.History },
            { nameof(SettingsStateHandler), WorkflowState.Settings }
        };

        if (!stateMapping.TryGetValue(callbackQuery.Data, out var newState))
            newState = session.State;

        session.Continue(newState);
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