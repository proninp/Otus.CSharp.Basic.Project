﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class TransactionSendCategoriesStateHandler : BaseSendCategoriesStateHandler
{
    public TransactionSendCategoriesStateHandler(
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

    protected override CategoryType GetCategoryType(UserSession session)
    {
        var context = session.GetTransactionContext();
        return context.TransactionType switch
        {
            TransactionType.Expense => CategoryType.Expense,
            TransactionType.Income => CategoryType.Income,
            _ => throw new InvalidOperationException(
                $"There is category type for the {context.TransactionType.GetDescription()} transaction type")
        };
    }
}