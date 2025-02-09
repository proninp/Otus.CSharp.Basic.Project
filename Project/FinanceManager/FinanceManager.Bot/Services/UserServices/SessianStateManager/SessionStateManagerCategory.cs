using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateManager : ISessionStateManager
{
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