using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram.Handlers;
public sealed class MenuCallbackHandler : IMenuCallbackHandler
{
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;

    public MenuCallbackHandler(
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager,
        ICallbackDataProvider callbackDataProvider)
    {
        _sessionStateManager = sessionStateManager;
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
    }

    public InlineKeyboardButton GetMenuButton(BotUpdateContext context)
    {
        var menu = NavigationCommand.Memu;
        var callbackData = new CallbackData(context, menu.GetCallbackData());
        var menuButton = _messageManager.CreateInlineButton(callbackData, $"{menu.GetSymbol()} {menu.GetDescription()}");
        return menuButton;
    }

    public async Task<bool> HandleMenuCallbackAsync(BotUpdateContext context)
    {
        var callbackData = await _callbackDataProvider.GetCallbackDataAsync(context, true);
        if (callbackData is not null)
        {
            var menuData = NavigationCommand.Memu.GetCallbackData();

            if (callbackData.Data == menuData)
            {
                await _messageManager.DeleteLastMessageAsync(context);
                await _sessionStateManager.ToMainMenu(context.Session);
                return true;
            }
        }
        return false;
    }
}