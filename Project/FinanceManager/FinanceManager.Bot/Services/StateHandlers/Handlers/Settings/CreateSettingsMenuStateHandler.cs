using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
public class CreateSettingsMenuStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IAccountInfoProvider _accountInfoProvider;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public CreateSettingsMenuStateHandler(
        IAccountManager accountManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IAccountInfoProvider accountInfoProvider,
        IMenuCallbackHandler menuCallbackProvider)
    {
        _accountManager = accountManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _accountInfoProvider = accountInfoProvider;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var message = updateContext.Session.LastMessage?.Text;
        if (message is null)
        {
            var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
            if (account is null)
                return await _sessionStateManager.Reset(updateContext.Session);
            message = await _accountInfoProvider.GetAccountInfoAsync(account, updateContext.CancellationToken);
        }
            
        var inlineKeyboard = CreateInlineKeyboard(updateContext);
        if (! await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var keyboardButtons = new List<InlineKeyboardButton[]>
        {
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(context, SettingsMenu.ManageCategories.GetKey(),
                    $"{Emoji.Category.GetSymbol()} {SettingsMenu.ManageCategories.GetDescription()}")
            },
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(context, SettingsMenu.ManageTransactios.GetKey(),
                    $"{Emoji.Money.GetSymbol()} {SettingsMenu.ManageTransactios.GetDescription()}")
            },
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(context, SettingsMenu.ManageAccounts.GetKey(),
                    $"{Emoji.Accounts.GetSymbol()} {SettingsMenu.ManageAccounts.GetDescription()}")
            },
            new InlineKeyboardButton[]
            {
                _menuCallbackProvider.GetMenuButton(context)
            }
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
