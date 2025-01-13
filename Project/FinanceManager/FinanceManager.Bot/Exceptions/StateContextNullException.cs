using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Exceptions;
public class StateContextNullException : ArgumentNullException
{
    public StateContextNullException() : base() { }

    public StateContextNullException(WorkflowState userState, string contextName)
        : base($"The {userState} state does not contain the {contextName} context.")
    {
    }

    public StateContextNullException(WorkflowState userState, string contextName, Exception? innerException)
        : base($"The {userState} state does not contain the {contextName} context.", innerException)
    {
    }
}