using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class SendCategoriesSubStateHandler : ISubStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public SendCategoriesSubStateHandler(
        ICategoryManager categoryManager, IChatProvider chatProvider, IMessageSenderManager messageSender)
    {
        _categoryManager = categoryManager;
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);
        var context = session.GetTransactionContext();

        var categories = await (context.TransactionType switch
        {
            TransactionType.Expense => _categoryManager.GetExpenses(session.Id, cancellationToken),
            TransactionType.Income => _categoryManager.GetIncomes(session.Id, cancellationToken),
            _ => throw new InvalidOperationException(
                $"There is no handler for the {context.TransactionType.GetDescription()} transaction type")
        });

        var inlineKeyboard = CreateInlineKeyboard(categories);

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat,
            $"Please choose the {context.TransactionTypeDescription} category {Emoji.Category.GetSymbol()}:",
            inlineKeyboard, cancellationToken);

        session.Wait(WorkflowSubState.ChooseTransactionCategory);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(CategoryDto[] categories)
    {
        var buttons = categories
            .Select(c => InlineKeyboardButton.WithCallbackData($"{c.Emoji} {c.Title}", c.Id.ToString()))
            .ToList();

        buttons.Add(InlineKeyboardButton.WithCallbackData($"{Emoji.Skip.GetSymbol()} Skip", Guid.Empty.ToString()));

        var keyboardButtons = buttons
            .Select((button, index) => new { button, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.button).ToArray())
            .ToArray();

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
