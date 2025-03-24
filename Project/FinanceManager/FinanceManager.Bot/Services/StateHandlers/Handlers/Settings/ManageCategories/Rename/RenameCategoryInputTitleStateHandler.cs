using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename
{
    public sealed class RenameCategoryInputTitleStateHandler : BaseInputCategoryTitleStateHandler
    {
        public RenameCategoryInputTitleStateHandler(
            IMessageManager messageManager,
            ISessionStateManager sessionStateManager,
            IMenuCallbackHandler menuCallbackProvider)
            : base(messageManager, sessionStateManager, menuCallbackProvider)
        {
        }

        private protected override string GetMessage() =>
            "Please enter a new name for the category";
    }
}
