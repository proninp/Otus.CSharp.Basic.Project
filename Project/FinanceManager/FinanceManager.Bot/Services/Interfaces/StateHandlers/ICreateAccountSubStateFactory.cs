using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ICreateAccountSubStateFactory
{
    ISubStateHandler GetSubStateHandler(UserSubState userSubState);
}