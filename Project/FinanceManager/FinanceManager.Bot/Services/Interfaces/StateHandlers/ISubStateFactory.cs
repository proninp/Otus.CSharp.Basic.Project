using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISubStateFactory
{
    IStateHandler GetSubStateHandler(UserState userState);
}