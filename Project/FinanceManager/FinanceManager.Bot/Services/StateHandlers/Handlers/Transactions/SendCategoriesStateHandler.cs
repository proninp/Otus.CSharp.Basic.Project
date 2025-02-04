using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class SendCategoriesStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public SendCategoriesStateHandler(
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
        var context = updateContext.Session.GetTransactionContext();

        var categories = await (context.TransactionType switch
        {
            TransactionType.Expense => _categoryManager.GetExpenses(updateContext.Session.Id, updateContext.CancellationToken),
            TransactionType.Income => _categoryManager.GetIncomes(updateContext.Session.Id, updateContext.CancellationToken),
            _ => throw new InvalidOperationException(
                $"There is no handler for the {context.TransactionType.GetDescription()} transaction type")
        });

        var inlineKeyboard = CreateInlineKeyboard(updateContext, categories);

        var message = $"Please choose the {context.TransactionTypeDescription} category {Emoji.Category.GetSymbol()}:";

        if (! await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

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