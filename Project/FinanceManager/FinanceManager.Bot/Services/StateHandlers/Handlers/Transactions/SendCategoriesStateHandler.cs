using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class SendCategoriesStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly IMessageManager _messageManager;

    public SendCategoriesStateHandler(
        ICategoryManager categoryManager, IMessageManager messageManager)
    {
        _categoryManager = categoryManager;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var categories = await (context.TransactionType switch
        {
            TransactionType.Expense => _categoryManager.GetExpenses(updateContext.Session.Id, updateContext.CancellationToken),
            TransactionType.Income => _categoryManager.GetIncomes(updateContext.Session.Id, updateContext.CancellationToken),
            _ => throw new InvalidOperationException(
                $"There is no handler for the {context.TransactionType.GetDescription()} transaction type")
        });

        var inlineKeyboard = CreateInlineKeyboard(categories);

        var message = $"Please choose the {context.TransactionTypeDescription} category {Emoji.Category.GetSymbol()}:";

        if (! await _messageManager.EditLastMessage(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.ChooseTransactionCategory);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(CategoryDto[] categories)
    {
        var buttons = categories
            .Select(c => InlineKeyboardButton.WithCallbackData($"{c.Emoji} {c.Title}", c.Id.ToString()))
            .ToList();

        buttons.Add(InlineKeyboardButton.WithCallbackData($"{Emoji.Skip.GetSymbol()} Skip", Guid.Empty.ToString()));

        var keyboardButtons = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 2)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToArray();

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
