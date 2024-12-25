using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IStateHandlerFactory
{
    IStateHandler GetHandler(UserState userState);
}
