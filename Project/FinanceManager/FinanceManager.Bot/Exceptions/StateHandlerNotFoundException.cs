using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Exceptions;
public class StateHandlerNotFoundException : InvalidOperationException
{
    public StateHandlerNotFoundException() : base() { }

    public StateHandlerNotFoundException(WorkflowState state)
        : base($"No handler found for the state: {state}.") 
    {
    }

    public StateHandlerNotFoundException(WorkflowState state, Exception? innerException)
        : base($"No handler found for the state: {state}.", innerException) 
    {
    }
}