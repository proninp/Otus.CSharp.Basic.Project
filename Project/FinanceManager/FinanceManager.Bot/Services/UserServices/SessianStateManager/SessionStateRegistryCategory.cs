using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateRegistry
{
    private void AddCreateCategoryWorkflowPrevious()
    {
        _previousStatesMap.TryAdd(WorkflowState.SetNewCategoryType, WorkflowState.SendNewCategoryType);
        _previousStatesMap.TryAdd(WorkflowState.SetNewCategoryName, WorkflowState.SendInputNewCategoryName);
        _previousStatesMap.TryAdd(WorkflowState.SetNewCategoryEmoji, WorkflowState.SendInputNewCategoryEmoji);
    }

    private void AddCreateCategoryWorkflowNext()
    {
        _nextStatesMap.TryAdd(WorkflowState.SendNewCategoryType, (WorkflowState.SetNewCategoryType, false));
        _nextStatesMap.TryAdd(WorkflowState.SetNewCategoryType, (WorkflowState.SendInputNewCategoryName, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputNewCategoryName, (WorkflowState.SetNewCategoryName, false));
        _nextStatesMap.TryAdd(WorkflowState.SetNewCategoryName, (WorkflowState.SendInputNewCategoryEmoji, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputNewCategoryEmoji, (WorkflowState.SetNewCategoryEmoji, false));
        _nextStatesMap.TryAdd(WorkflowState.SetNewCategoryEmoji, (WorkflowState.RegisterNewCategory, true));
        _nextStatesMap.TryAdd(WorkflowState.RegisterNewCategory, (WorkflowState.CreateManageCategoriesMenu, true));
    }

    private void AddRemoveCategoryWorkflowPrevious()
    {
        _previousStatesMap.TryAdd(WorkflowState.SetDeletingCategoryType, WorkflowState.SendChooseDeletingCategoryType);
        _previousStatesMap.TryAdd(WorkflowState.ChooseCategoryToDelete, WorkflowState.SendChooseCategoryToDelete);
        _previousStatesMap.TryAdd(WorkflowState.HandleDeletingCategoryConfirmation, WorkflowState.SendDeletingCategoryConfirmation);
    }

    private void AddRemoveCategoryWorkflowNext()
    {
        _nextStatesMap.TryAdd(WorkflowState.SendChooseDeletingCategoryType, (WorkflowState.SetDeletingCategoryType, false));
        _nextStatesMap.TryAdd(WorkflowState.SetDeletingCategoryType, (WorkflowState.SendChooseCategoryToDelete, true));
        _nextStatesMap.TryAdd(WorkflowState.SendChooseCategoryToDelete, (WorkflowState.ChooseCategoryToDelete, false));
        _nextStatesMap.TryAdd(WorkflowState.ChooseCategoryToDelete, (WorkflowState.SendDeletingCategoryConfirmation, true));
        _nextStatesMap.TryAdd(WorkflowState.SendDeletingCategoryConfirmation, (WorkflowState.HandleDeletingCategoryConfirmation, false));
        _nextStatesMap.TryAdd(WorkflowState.HandleDeletingCategoryConfirmation, (WorkflowState.RegisterDeleteCategory, true));
        _nextStatesMap.TryAdd(WorkflowState.RegisterDeleteCategory, (WorkflowState.CreateManageCategoriesMenu, true));
    }

    private void AddRenameCategoryWorkflowPrevious()
    {
        _previousStatesMap.TryAdd(WorkflowState.SetRenamingCategoryType, WorkflowState.SendChooseRenamingCategoryType);
        _previousStatesMap.TryAdd(WorkflowState.ChooseCategoryToRename, WorkflowState.SendChooseRenamingCategory);
        _previousStatesMap.TryAdd(WorkflowState.SetCategoryName, WorkflowState.SendInputCategoryName);
        _previousStatesMap.TryAdd(WorkflowState.SetCategoryEmoji, WorkflowState.SendInputCategoryEmoji);
    }

    private void AddRenameCategoryWorkflowNext()
    {
        _nextStatesMap.TryAdd(WorkflowState.SendChooseRenamingCategoryType, (WorkflowState.SetRenamingCategoryType, false));
        _nextStatesMap.TryAdd(WorkflowState.SetRenamingCategoryType, (WorkflowState.SendChooseRenamingCategory, true));
        _nextStatesMap.TryAdd(WorkflowState.SendChooseRenamingCategory, (WorkflowState.ChooseCategoryToRename, false));
        _nextStatesMap.TryAdd(WorkflowState.ChooseCategoryToRename, (WorkflowState.SendInputCategoryName, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputCategoryName, (WorkflowState.SetCategoryName, false));
        _nextStatesMap.TryAdd(WorkflowState.SetCategoryName, (WorkflowState.SendInputCategoryEmoji, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputCategoryEmoji, (WorkflowState.SetCategoryEmoji, false));
        _nextStatesMap.TryAdd(WorkflowState.SetCategoryEmoji, (WorkflowState.RegisterRenameCategory, true));
        _nextStatesMap.TryAdd(WorkflowState.RegisterRenameCategory, (WorkflowState.CreateManageCategoriesMenu, true));
    }
}
