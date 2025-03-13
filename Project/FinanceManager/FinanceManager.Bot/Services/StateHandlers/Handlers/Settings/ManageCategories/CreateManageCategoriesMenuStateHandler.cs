using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
public sealed class CreateManageCategoriesMenuStateHandler : IStateHandler
{

    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public CreateManageCategoriesMenuStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var message = "Choose what to do with categories:";

        var inlineKeyboard = CreateInlineKeyboard(updateContext);

        if (!await _messageManager.EditLastMessageAsync(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessageAsync(updateContext, message, inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var keyboardButtons = new List<InlineKeyboardButton[]>
        {
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(context, ManageCategoriesMenu.Add.GetKey(),
                $"{Emoji.Add.GetSymbol()} {ManageCategoriesMenu.Add.GetDescription()}"),

                _messageManager.CreateInlineButton(context, ManageCategoriesMenu.Delete.GetKey(),
                $"{Emoji.Delete.GetSymbol()} {ManageCategoriesMenu.Delete.GetDescription()}"),

                _messageManager.CreateInlineButton(context, ManageCategoriesMenu.Rename.GetKey(),
                $"{Emoji.Change.GetSymbol()} {ManageCategoriesMenu.Rename.GetDescription()}")
            },
            new InlineKeyboardButton[]
            {
                _menuCallbackProvider.GetMenuButton(context)
            }
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
