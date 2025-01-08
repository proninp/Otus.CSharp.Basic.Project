using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISubStateFactory
{
    ISubStateHandler GetSubStateHandler(WorkflowSubState userSubState);
}