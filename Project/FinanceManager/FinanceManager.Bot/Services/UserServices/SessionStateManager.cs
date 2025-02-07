using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Redis.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class SessionStateManager : ISessionStateManager
{
    private readonly IRedisCacheService _redisCacheService;

    public SessionStateManager(IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    public async Task<bool> Previous(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, WorkflowState>
        {
            { WorkflowState.SelectMenu, WorkflowState.CreateMenu },
            { WorkflowState.SelectSettingsMenu, WorkflowState.CreateSettingsMenu },
            { WorkflowState.SelectManageCategoriesMenu, WorkflowState.CreateManageCategoriesMenu },
        };

        AddCreateAccountWorkflowPrevious(stateTransitions);
        AddRegistrTransactionWorkflowPrevious(stateTransitions);
        AddCreateCategoryWorkflowPrevious(stateTransitions);
        AddRemoveCategoryWorkflowPrevious(stateTransitions);
        AddRenameCategoryWorkflowPrevious(stateTransitions);

        if (stateTransitions.TryGetValue(session.State, out var toState))
        {
            await SetState(session, toState);
        }
        else
        {
            await Reset(session);
        }
        return true;
    }

    public async Task<bool> Next(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, (WorkflowState next, bool isContinue)>
        {
            { WorkflowState.CreateMenu, (WorkflowState.SelectMenu, false) },
            { WorkflowState.CreateSettingsMenu, (WorkflowState.SelectSettingsMenu, false) },
            { WorkflowState.CreateManageCategoriesMenu, (WorkflowState.SelectManageCategoriesMenu, false) },
        };

        AddCreateAccountWorkflowNext(stateTransitions);
        AddRegisterTransactionWorkflowNext(stateTransitions);
        AddCreateCategoryWorkflowNext(stateTransitions);
        AddRemoveCategoryWorkflowNext(stateTransitions);
        AddRenameCategoryWorkflowNext(stateTransitions);

        bool result;

        if (stateTransitions.TryGetValue(session.State, out var sessionBehavior))
        {
            await SetState(session, sessionBehavior.next);
            result = sessionBehavior.isContinue;
        }
        else
        {
            result = await Reset(session);
        }
        return result;
    }

    public async Task<bool> ToMainMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateMenu);

    public async Task<bool> ToSettingsMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateSettingsMenu);

    public async Task<bool> InitAccount(UserSession session) =>
        await Continue(session, WorkflowState.CreateAccountStart);

    public async Task<bool> FromMenu(UserSession session, WorkflowState toState) =>
        await Continue(session, toState);

    public async Task<bool> Reset(UserSession session) =>
        await Continue(session, WorkflowState.Default);

    private async Task<bool> Continue(UserSession session, WorkflowState withState)
    {
        session.WorkflowContext = null;
        await SetState(session, withState);
        return true;
    }

    public async Task<bool> Complete(UserSession session) => session.State switch
    {
        WorkflowState.RegisterTransaction => await ToMainMenu(session),
        WorkflowState.RegisterNewCategory => await Continue(session, WorkflowState.ManageAccounts),
        _ => await Reset(session)
    };

    private async Task SetState(UserSession session, WorkflowState newState)
    {
        session.State = newState;
        session.LastActivity = DateTime.UtcNow;
        await _redisCacheService.SaveDataAsync(session.TelegramId.ToString(), session);
    }

    private void AddCreateAccountWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.ChooseAccountName, WorkflowState.CreateAccountStart);
        workflowMap.Add(WorkflowState.ChooseCurrency, WorkflowState.SendCurrencies);
        workflowMap.Add(WorkflowState.SetAccountInitialBalance, WorkflowState.SendInputAccountInitialBalance);
    }

    private void AddCreateAccountWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, false));
        workflowMap.Add(WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, true));
        workflowMap.Add(WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, false));
        workflowMap.Add(WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, true));
        workflowMap.Add(WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, false));
        workflowMap.Add(WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountEnd, true));
        workflowMap.Add(WorkflowState.CreateAccountEnd, (WorkflowState.CreateMenu, true));
    }

    private void AddRegistrTransactionWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.ChooseTransactionCategory, WorkflowState.SendTransactionCategories);
        workflowMap.Add(WorkflowState.SetTransactionDate, WorkflowState.SendInputTransactionDate);
        workflowMap.Add(WorkflowState.SetTransactionAmount, WorkflowState.SendInputTransactionAmount);
        workflowMap.Add(WorkflowState.History, WorkflowState.CreateMenu);
    }

    private void AddRegisterTransactionWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, true));
        workflowMap.Add(WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, true));
        workflowMap.Add(WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, false));
        workflowMap.Add(WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, true));
        workflowMap.Add(WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, false));
        workflowMap.Add(WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, true));
        workflowMap.Add(WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, false));
        workflowMap.Add(WorkflowState.SetTransactionAmount, (WorkflowState.RegisterTransaction, true));
        workflowMap.Add(WorkflowState.RegisterTransaction, (WorkflowState.CreateMenu, true));
    }

    private void AddCreateCategoryWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.SetNewCategoryType, WorkflowState.SendNewCategoryType);
        workflowMap.Add(WorkflowState.SetNewCategoryName, WorkflowState.SendInputNewCategoryName);
        workflowMap.Add(WorkflowState.SetNewCategoryEmoji, WorkflowState.SendInputNewCategoryEmoji);
    }

    private void AddCreateCategoryWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.SendNewCategoryType, (WorkflowState.SetNewCategoryType, false));
        workflowMap.Add(WorkflowState.SetNewCategoryType, (WorkflowState.SendInputNewCategoryName, true));
        workflowMap.Add(WorkflowState.SendInputNewCategoryName, (WorkflowState.SetNewCategoryName, false));
        workflowMap.Add(WorkflowState.SetNewCategoryName, (WorkflowState.SendInputNewCategoryEmoji, true));
        workflowMap.Add(WorkflowState.SendInputNewCategoryEmoji, (WorkflowState.SetNewCategoryEmoji, false));
        workflowMap.Add(WorkflowState.SetNewCategoryEmoji, (WorkflowState.RegisterNewCategory, true));
        workflowMap.Add(WorkflowState.RegisterNewCategory, (WorkflowState.ManageCategories, true));
    }

    private void AddRemoveCategoryWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.SetDeletingCategoryType, WorkflowState.SendChooseDeletingCategoryType);
        workflowMap.Add(WorkflowState.ChooseCategoryToDelete, WorkflowState.SendChooseCategoryToDelete);
        workflowMap.Add(WorkflowState.HandleDeletingCategoryConfirmation, WorkflowState.SendDeletingCategoryConfirmation);
    }

    private void AddRemoveCategoryWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.SendChooseDeletingCategoryType, (WorkflowState.SetDeletingCategoryType, false));
        workflowMap.Add(WorkflowState.SetDeletingCategoryType, (WorkflowState.SendChooseCategoryToDelete, true));
        workflowMap.Add(WorkflowState.SendChooseCategoryToDelete, (WorkflowState.ChooseCategoryToDelete, false));
        workflowMap.Add(WorkflowState.ChooseCategoryToDelete, (WorkflowState.SendDeletingCategoryConfirmation, true));
        workflowMap.Add(WorkflowState.SendDeletingCategoryConfirmation, (WorkflowState.HandleDeletingCategoryConfirmation, false));
        workflowMap.Add(WorkflowState.HandleDeletingCategoryConfirmation, (WorkflowState.RegisterDeleteCategory, true));
        workflowMap.Add(WorkflowState.RegisterDeleteCategory, (WorkflowState.ManageCategories, true));
    }

    private void AddRenameCategoryWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.SetRenamingCategoryType, WorkflowState.SendChooseRenamingCategoryType);
        workflowMap.Add(WorkflowState.ChooseCategoryToRename, WorkflowState.SendChooseRenamingCategory);
        workflowMap.Add(WorkflowState.SetCategoryName, WorkflowState.SendInputCategoryName);
        workflowMap.Add(WorkflowState.SetCategoryEmoji, WorkflowState.SendInputCategoryEmoji);
    }

    private void AddRenameCategoryWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.SendChooseRenamingCategoryType, (WorkflowState.SetRenamingCategoryType, false));
        workflowMap.Add(WorkflowState.SetRenamingCategoryType, (WorkflowState.SendChooseRenamingCategory, true));
        workflowMap.Add(WorkflowState.SendChooseRenamingCategory, (WorkflowState.ChooseCategoryToRename, false));
        workflowMap.Add(WorkflowState.ChooseCategoryToRename, (WorkflowState.SendInputCategoryName, true));
        workflowMap.Add(WorkflowState.SendInputCategoryName, (WorkflowState.SetCategoryName, false));
        workflowMap.Add(WorkflowState.SetCategoryName, (WorkflowState.SendInputCategoryEmoji, true));
        workflowMap.Add(WorkflowState.SendInputCategoryEmoji, (WorkflowState.SetCategoryEmoji, false));
        workflowMap.Add(WorkflowState.SetCategoryEmoji, (WorkflowState.RegisterRenameCategory, true));
        workflowMap.Add(WorkflowState.RegisterRenameCategory, (WorkflowState.ManageCategories, true));
    }    
}