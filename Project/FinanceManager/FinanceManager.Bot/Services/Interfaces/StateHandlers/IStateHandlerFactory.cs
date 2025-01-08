using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IStateHandlerFactory
{
    IStateHandler GetHandler(UserState userState);
}
