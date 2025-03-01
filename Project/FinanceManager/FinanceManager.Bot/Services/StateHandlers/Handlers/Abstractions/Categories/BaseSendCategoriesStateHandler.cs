using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Core.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
public abstract class BaseSendCategoriesStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    protected BaseSendCategoriesStateHandler(
        ICategoryManager categoryManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
    {
        _categoryManager = categoryManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var session = updateContext.Session;
        var categoryType = GetCategoryType(updateContext.Session);
        var categories = await (categoryType switch
        {
            CategoryType.Expense => _categoryManager.GetExpenses(session.Id, updateContext.CancellationToken),
            CategoryType.Income => _categoryManager.GetIncomes(session.Id, updateContext.CancellationToken),
            _ => throw new InvalidOperationException(
                $"There is no handler for the {categoryType.GetDescription()} transaction type")
        });

        var message = GetMessageText(session);
        var inlineKeyboard = CreateInlineKeyboard(updateContext, categories);

        if (!await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        return await _sessionStateManager.Next(session);
    }

    protected abstract CategoryType GetCategoryType(UserSession session);

    protected abstract string GetMessageText(UserSession session);

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context, CategoryDto[] categories)
    {
        var buttons = categories
            .Select(c => _messageManager.CreateInlineButton(context, c.Id.ToString(), $"{c.Emoji} {c.Title}"))
            .ToList();

        buttons.Add(
            _messageManager.CreateInlineButton(context, Guid.Empty.ToString(), $"{Emoji.Skip.GetSymbol()} Skip"));

        var menuButton = _menuCallbackProvider.GetMenuButton(context);

        var keyboardButtons = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 2)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToList();

        keyboardButtons.Add([menuButton]);

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
