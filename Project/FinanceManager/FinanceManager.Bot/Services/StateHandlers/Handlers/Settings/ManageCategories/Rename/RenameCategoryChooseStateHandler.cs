using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename;

public sealed class RenameCategoryChooseStateHandler : BaseChooseCategoryStateHandler
{
    public RenameCategoryChooseStateHandler(
        ICategoryManager categoryManager,
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
        : base(categoryManager, callbackDataProvider, messageManager, sessionStateManager)
    {
    }

    protected override void SaveCategoryToContext(UserSession session, CategoryDto? category)
    {
        session.GetRenameCategoryContext().Category = category;
    }
}