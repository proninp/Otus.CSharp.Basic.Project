using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IStateHandlerFactory
{
    IStateHandler GetHandler(WorkflowState state);
}
