using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;

public sealed class DeleteCategorySendConfirm : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly ITransactionManager _transactionManager;

    public DeleteCategorySendConfirm(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ITransactionManager transactionManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _transactionManager = transactionManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var session = updateContext.Session;
        var context = session.GetDeleteCategoryContext();

        if (context.Category is null)
        {
            await _messageManager.DeleteLastMessage(updateContext);
            await _messageManager.SendErrorMessage(
                updateContext, "An error occurred while performing the category deletion operation, please start from the beginning");
            return await _sessionStateManager.ToManageCategoriesMenu(session);
        }

        var message = new StringBuilder($"Do you really want to delete the category {context.Category.Title}");
        if (!string.IsNullOrWhiteSpace(context.Category.Emoji))
            message.Append($" {context.Category.Emoji}");
        message.Append('?');

        var transactionsCount = await _transactionManager.GetCount(
            session.Id, default, context.Category.Id, updateContext.CancellationToken);
        if (transactionsCount > 0)
        {
            message
                .AppendLine()
                .Append($"Transactions were registered for this category: {transactionsCount}.");
        }

        var inlineKeyboard = CreateInlineKeyboard(updateContext);

        if (!await _messageManager.EditLastMessage(updateContext, message.ToString(), inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message.ToString(), inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext updateContext)
    {
        var buttons = new List<InlineKeyboardButton[]>()
        {
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(updateContext, true.ToString(), "Yes"),
                _messageManager.CreateInlineButton(updateContext, false.ToString(), "No")
            }
        };
        return new InlineKeyboardMarkup(buttons);
    }
}
