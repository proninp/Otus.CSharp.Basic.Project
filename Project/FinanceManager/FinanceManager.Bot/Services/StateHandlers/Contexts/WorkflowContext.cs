using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public sealed class WorkflowContext
{
    public CreateAccountContext? CreateAccountContext { get; private set; }

    public HistoryContext? HistoryContext { get; private set; }

    public TransactionContext? TransactionContext { get; private set; }

    public CreateCategoryContext? CreateCategoryContext { get; private set; }
    
    public DeleteCategoryContext? DeleteCategoryContext { get; private set; }

    public RenameCategoryContext? RenameCategoryContext { get; private set; }

    public WorkflowContext() { }

    public WorkflowContext(CreateAccountContext createAccountContext) =>
        CreateAccountContext = createAccountContext;

    public WorkflowContext(HistoryContext historyContext) =>
        HistoryContext = historyContext;

    public WorkflowContext(TransactionContext transactionContext) =>
        TransactionContext = transactionContext;

    public WorkflowContext(CreateCategoryContext createCategoryContext) =>
        CreateCategoryContext = createCategoryContext;
    
    public WorkflowContext(DeleteCategoryContext deleteCategoryContext) =>
        DeleteCategoryContext = deleteCategoryContext;

    public WorkflowContext(RenameCategoryContext renameCategoryContext) =>
        RenameCategoryContext = renameCategoryContext;

    public void ResetContext()
    {
        CreateAccountContext = null;
        HistoryContext = null;
        TransactionContext = null;
        CreateCategoryContext = null;
        DeleteCategoryContext = null;
        RenameCategoryContext = null;
    }
}

public static class WorkflowContextExtension
{
    public static void SetCreateAccountContext(this UserSession userSession, CreateAccountContext createAccountContext) =>
        userSession.WorkflowContext = new WorkflowContext(createAccountContext);

    public static void SetTransactionContext(this UserSession userSession, TransactionContext transactionContext) =>
        userSession.WorkflowContext = new WorkflowContext(transactionContext);

    public static void SetHistoryContext(this UserSession userSession, HistoryContext historyContext) =>
        userSession.WorkflowContext = new WorkflowContext(historyContext);

    public static void SetCreateCategoryContext(this UserSession userSession, CreateCategoryContext createCategoryContext) =>
        userSession.WorkflowContext = new WorkflowContext(createCategoryContext);

    public static void SetDeleteCategoryContext(this UserSession userSession, DeleteCategoryContext deleteCategoryContext) =>
        userSession.WorkflowContext = new WorkflowContext(deleteCategoryContext);

    public static void SetRenameCategoryContext(this UserSession userSession, RenameCategoryContext renameCategoryContext) =>
        userSession.WorkflowContext = new WorkflowContext(renameCategoryContext);
}