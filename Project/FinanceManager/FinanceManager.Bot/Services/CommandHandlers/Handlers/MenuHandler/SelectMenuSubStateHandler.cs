using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.MenuHandler;
public class SelectMenuSubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;

    public SelectMenuSubStateHandler(IChatProvider chatProvider, IUpdateCallbackQueryProvider updateCallbackQueryProvider)
    {
        _chatProvider = chatProvider;
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var currentSubSate = session.SubState;
        if (!_updateCallbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
            return currentSubSate;

        var state = session.UserState;

        switch (callbackQuery.Data)
        {
            case nameof(RegisterExpenseStateHandler):
                {
                    state = UserState.AddExpense;
                    break;
                }
            case nameof(RegisterIncomeStateHandler):
                {
                    state = UserState.AddIncome;
                    break;
                }
            case nameof(HistoryStateHandler):
                {
                    state = UserState.History;
                    break;
                }
            case nameof(SettingsStateHandler):
                {
                    state = UserState.Settings;
                    break;
                }
            default:
                return currentSubSate;
        }
        session.ResetState();
        session.UserState = state;
        return session.SubState;
    }
}
