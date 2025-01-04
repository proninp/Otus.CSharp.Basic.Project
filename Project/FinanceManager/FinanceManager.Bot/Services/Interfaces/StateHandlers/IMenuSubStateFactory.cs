using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IMenuSubStateFactory
{
    ISubStateHandler GetSubStateHandler(UserSubState userSubState);
}
