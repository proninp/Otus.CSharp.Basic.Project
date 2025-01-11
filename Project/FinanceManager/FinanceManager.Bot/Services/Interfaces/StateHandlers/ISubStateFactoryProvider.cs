using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISubStateFactoryProvider
{
    ISubStateFactory GetSubStateFactory(UserState userState);
}