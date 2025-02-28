using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;

public class DeleteChooseCategoryTypeStateHandler : BaseChooseCategoryTypeStateHandler
{
    public DeleteChooseCategoryTypeStateHandler(
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
        : base(callbackDataProvider, messageManager, sessionStateManager)
    {
    }

    protected override void SaveCategoryToContext(UserSession session, CategoryType categoryType) =>
        session.SetDeleteCategoryContext(DeleteCategoryContext.CreateContext(categoryType));
}
