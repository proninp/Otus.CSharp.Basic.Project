using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISubStateFactoryProvider
{
    ISubStateFactory GetSubStateFactory(WorkflowState state);
}