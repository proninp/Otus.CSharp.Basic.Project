﻿using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategorySetTitleStateHandler : BaseSetCategoryTitleStateHandler
{
    public CreateCategorySetTitleStateHandler(
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ICategoryTitleValidator categoryTitleValidator)
        : base(messageProvider, messageManager, sessionStateManager, categoryTitleValidator)
    {
    }

    private protected override void SetNewTitleToContext(UserSession session, string newTitle) =>
        session.GetCreateCategoryContext().CategoryTitle = newTitle;
}