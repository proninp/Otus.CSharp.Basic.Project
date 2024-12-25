using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces;
public interface ISubStateHandlerFactory
{
    ISubStateHandler GetSubStateHandler(UserSubState userSubState);
}
