using System.Collections.Concurrent;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces;
public interface ISessionStateRegistry
{
    ConcurrentDictionary<WorkflowState, WorkflowState> PreviousStatesMap { get; }

    ConcurrentDictionary<WorkflowState, (WorkflowState next, bool isContinue)> NextStatesMap { get; }
}
