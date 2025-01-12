using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Exceptions;
public class BotStateHandlerFactoryException : InvalidOperationException
{
    public BotStateHandlerFactoryException() : base() { }

    public BotStateHandlerFactoryException(BotState state)
        : base($"No handler found for the state: {state}.")
    {
    }

    public BotStateHandlerFactoryException(BotState state, Exception? innerException)
        : base($"No handler found for the state: {state}.", innerException)
    {
    }
}