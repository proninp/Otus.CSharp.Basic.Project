using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;
public sealed class DeleteCategoryStartStateHandler : BaseSendCategoriesTypeStateHandler
{
    public DeleteCategoryStartStateHandler(
        IMessageManager messageManager,
        IMenuCallbackHandler menuCallbackProvider,
        ISessionStateManager sessionStateManager)
        : base(messageManager, menuCallbackProvider, sessionStateManager)
    {
    }

    protected override string GetMessageText(UserSession session) =>
        "Please select the type of category to be deleted from";
}
