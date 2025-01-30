using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public sealed class WorkflowContext
{
    public CreateAccountContext? CreateAccountContext { get; set; }

    public HistoryContext? HistoryContext { get; set; }

    public TransactionContext? TransactionContext { get; set; }

    public WorkflowContext() { }

    public WorkflowContext(CreateAccountContext createAccountContext)
    {
        CreateAccountContext = createAccountContext;
    }

    public WorkflowContext(HistoryContext historyContext)
    {
        HistoryContext = historyContext;
    }

    public WorkflowContext(TransactionContext transactionContext)
    {
        TransactionContext = transactionContext;
    }

    public void ResetContext()
    {
        CreateAccountContext = null;
        HistoryContext = null;
        TransactionContext = null;
    }
}

public static class WorkflowContextExtension
{
    public static void SetCreateAccountContext(this UserSession userSession, CreateAccountContext createAccountContext)
    {
        userSession.WorkflowContext = new WorkflowContext(createAccountContext);
    }

    public static void SetTransactionContext(this UserSession userSession, TransactionContext transactionContext)
    {
        userSession.WorkflowContext = new WorkflowContext(transactionContext);
    }

    public static void SetHistoryContext(this UserSession userSession, HistoryContext historyContext)
    {
        userSession.WorkflowContext = new WorkflowContext(historyContext);
    }


}
