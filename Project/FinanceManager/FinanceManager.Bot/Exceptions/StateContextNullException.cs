using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Exceptions;
public class StateContextNullException : ArgumentNullException
{
    public StateContextNullException() : base() { }

    public StateContextNullException(UserState userState, string contextName)
        : base($"The {userState.State} state does not contain the {contextName} context.")
    {
    }

    public StateContextNullException(UserState userState, string contextName, Exception? innerException)
        : base($"The {userState.State} state does not contain the {contextName} context.", innerException)
    {
    }
}