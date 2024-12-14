using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IStateHandlerFactory
{
    ICommandStateHandler GetHandler(UserState userState);
}
