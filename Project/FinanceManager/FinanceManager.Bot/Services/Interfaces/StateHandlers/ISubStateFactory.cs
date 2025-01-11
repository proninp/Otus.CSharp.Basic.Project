using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISubStateFactory
{
    ISubStateHandler GetSubStateHandler(UserState userState);
}