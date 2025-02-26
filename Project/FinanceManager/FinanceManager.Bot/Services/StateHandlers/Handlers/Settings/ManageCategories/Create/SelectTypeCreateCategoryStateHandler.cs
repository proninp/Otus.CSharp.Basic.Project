using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class SelectTypeCreateCategoryStateHandler : BaseSelectTypeCategoryStateHandler
{
    public SelectTypeCreateCategoryStateHandler(
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
        : base(callbackDataProvider, messageManager, sessionStateManager)
    {
    }

    protected override void SaveCategoryToContext(UserSession session, CategoryType categoryType) =>
        session.SetCreateCategoryContext(CreateCategoryContext.CreateContext(categoryType));
}
