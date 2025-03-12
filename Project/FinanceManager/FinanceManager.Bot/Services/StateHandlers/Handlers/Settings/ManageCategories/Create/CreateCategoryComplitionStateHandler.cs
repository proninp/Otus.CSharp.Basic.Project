using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategoryComplitionStateHandler : CompleteStateHandler
{
    private readonly ICategoryManager _categoryManager;
    public CreateCategoryComplitionStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ICategoryManager categoryManager)
        : base(messageManager, sessionStateManager)
    {
        _categoryManager = categoryManager;
    }

    private protected override async Task HandleCompleteAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetCreateCategoryContext();

        var command = new CreateCategoryDto
        {
            UsertId = updateContext.Session.Id,
            CategoryType = context.CategoryType,
            Title = context.CategoryTitle,
            Emoji = context.Emoji,
        };

        await _categoryManager.Create(command, updateContext.CancellationToken);

        var message = $"The category '{context.CategoryTitle}' was successfully created!";

        await _messageManager.SendApproveMessage(updateContext, message);
    }
}