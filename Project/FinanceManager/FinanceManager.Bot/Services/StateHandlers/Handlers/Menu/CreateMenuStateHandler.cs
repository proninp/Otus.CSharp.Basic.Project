using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public sealed class CreateMenuStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IAccountInfoProvider _accountInfoProvider;

    public CreateMenuStateHandler(
        IMessageManager messageManager,
        IAccountManager accountManager,
        ISessionStateManager sessionStateManager,
        IAccountInfoProvider accountInfoProvider)
    {
        _messageManager = messageManager;
        _accountManager = accountManager;
        _sessionStateManager = sessionStateManager;
        _accountInfoProvider = accountInfoProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
            return await _sessionStateManager.Reset(updateContext.Session);

        var messageText = await _accountInfoProvider.GetAccountInfoAsync(account, updateContext.CancellationToken);
        var inlineKeyboard = CreateInlineKeyboard(updateContext);

        await _messageManager.SendInlineKeyboardMessage(updateContext, messageText, inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                _messageManager.CreateInlineButton(context, MainMenu.Expense.GetKey(),
                    $"{Emoji.Expense.GetSymbol()} {MainMenu.Expense.GetDescription()}"),

                _messageManager.CreateInlineButton(context, MainMenu.Income.GetKey(),
                    $"{Emoji.Income.GetSymbol()} {MainMenu.Income.GetDescription()}"),
            ],
            [
                _messageManager.CreateInlineButton(context, MainMenu.History.GetKey(),
                    $"{Emoji.History.GetSymbol()} {MainMenu.History.GetDescription()}"),

                _messageManager.CreateInlineButton(context, MainMenu.Settings.GetKey(),
                    $"{Emoji.Settings.GetSymbol()} {MainMenu.Settings.GetDescription()}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
