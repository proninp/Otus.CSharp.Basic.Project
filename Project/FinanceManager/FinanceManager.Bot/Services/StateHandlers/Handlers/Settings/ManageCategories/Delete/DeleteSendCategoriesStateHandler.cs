using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;

public sealed class DeleteSendCategoriesStateHandler : BaseSendCategoriesStateHandler
{
    public DeleteSendCategoriesStateHandler(
        ICategoryManager categoryManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
        : base(categoryManager, messageManager, sessionStateManager, menuCallbackProvider)
    {
    }

    protected override string GetMessageText(UserSession session)
    {
        var context = session.GetDeleteCategoryContext();
        return $"Please choose a category {Emoji.Category.GetSymbol()} to delete:";
    }

    protected override CategoryType GetCategoryType(UserSession session)
    {
        var context = session.GetDeleteCategoryContext();
        return context.CategoryType;
    }
}