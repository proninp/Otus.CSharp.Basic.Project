using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IAccountSubStateHandlerFactory
{
    ISubStateHandler GetSubStateHandler(UserSubState userSubState);
}