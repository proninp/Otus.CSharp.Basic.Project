using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategoryInputTitleStateHandler : BaseInputCategoryTitleStateHandler
{
    public CreateCategoryInputTitleStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
        : base(messageManager, sessionStateManager, menuCallbackProvider)
    {
    }

    private protected override string GetMessage() =>
        "Please enter the title of the new category";
}
