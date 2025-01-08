using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserState
{
    private bool _waitForUserInput;

    public WorkflowState State { get; set; } = WorkflowState.Default;

    public WorkflowSubState SubState { get; set; } = WorkflowSubState.Default;

    public void Reset()
    {
        State = WorkflowState.Default;
        SubState = WorkflowSubState.Default;
        _waitForUserInput = false;
    }

    public bool IsContinue()
    {
        var wait = _waitForUserInput;
        _waitForUserInput = false;
        return !wait;
    }

    public void Wait() =>
        _waitForUserInput = true;

    public void Wait(WorkflowSubState subState)
    {
        SubState = subState;
        _waitForUserInput = true;
    }

    public void Wait(WorkflowState state)
    {
        State = state;
        SubState = WorkflowSubState.Default;
        _waitForUserInput = true;
    }

    public void Continue() =>
        _waitForUserInput = false;

    public void Continue(WorkflowSubState subState)
    {
        SubState = subState;
        _waitForUserInput = false;
    }

    public void Continue(WorkflowState state)
    {
        State = state;
        SubState = WorkflowSubState.Default;
        _waitForUserInput = false;
    }

    public override bool Equals(object? obj)
    {
        return obj is UserState state &&
               State == state.State &&
               SubState == state.SubState;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(State, SubState);
    }
}
