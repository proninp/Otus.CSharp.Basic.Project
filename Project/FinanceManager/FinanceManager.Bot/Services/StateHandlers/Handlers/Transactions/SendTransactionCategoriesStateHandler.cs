using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class SendTransactionCategoriesStateHandler : BaseSendCategoriesStateHandler
{
    public SendTransactionCategoriesStateHandler(
        ICategoryManager categoryManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
        : base(categoryManager, messageManager, sessionStateManager, menuCallbackProvider)
    {
    }

    protected override string GetMessageText(UserSession session)
    {
        var context = session.GetTransactionContext();
        return $"Please choose the {context.TransactionTypeDescription} category {Emoji.Category.GetSymbol()}:";
    }

    protected override TransactionType GetTransactionType(UserSession session)
    {
        var context = session.GetTransactionContext();
        return context.TransactionType;
    }
}