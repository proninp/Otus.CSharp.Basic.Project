using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class ChooseCategorySubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageSenderManager _messageSender;
    private readonly ICategoryManager _categoryManager;

    public ChooseCategorySubStateHandler(
        IChatProvider chatProvider,
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageSenderManager messageSender,
        ICategoryManager categoryManager)
    {
        _chatProvider = chatProvider;
        _callbackQueryProvider = callbackQueryProvider;
        _messageSender = messageSender;
        _categoryManager = categoryManager;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var previousState = WorkflowSubState.Default;
        if (!_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
        {
            session.Continue(previousState);
            return;
        }

        var categoryId = callbackQuery.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            session.Continue(previousState);
            return;
        }    

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetById(new Guid(categoryId), cancellationToken);
            if (category is null)
            {
                session.Continue(previousState);
                return;
            }
        }

        var context = session.GetTransactionContext();
        context.Category = category;

        var chat = _chatProvider.GetChat(update);

        await _messageSender.SendMessage(
            botClient, chat,
            $"Please enter the date (dd mm yyyy) of the {context.TransactionTypeDescription} {Emoji.Calendar.GetSymbol()}:",
            cancellationToken);

        session.Wait(WorkflowSubState.SetTransactionDate);
    }
}
