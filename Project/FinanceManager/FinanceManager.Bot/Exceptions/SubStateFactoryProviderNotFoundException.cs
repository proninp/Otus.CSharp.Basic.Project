using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Exceptions;
public class SubStateFactoryProviderNotFoundException : InvalidOperationException
{
    public SubStateFactoryProviderNotFoundException() : base() { }

    public SubStateFactoryProviderNotFoundException(WorkflowState state)
        : base($"No substate factrory provider found for the state: {state}.")
    {
    }

    public SubStateFactoryProviderNotFoundException(WorkflowState state, Exception? innerException)
        : base($"No substate factrory provider found for the state: {state}.", innerException)
    {
    }
}
