using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Exceptions;
public class SubStateHandlerNotFoundException : InvalidOperationException
{
    public SubStateHandlerNotFoundException() : base() { }

    public SubStateHandlerNotFoundException(WorkflowState state, WorkflowSubState subState)
        : base($"No handler found for the substate: {subState} in state {state}.")
    {
    }

    public SubStateHandlerNotFoundException(WorkflowState state, WorkflowSubState subState, Exception? innerException)
        : base($"No handler found for the substate: {subState} in state {state}.", innerException)
    {
    }
}