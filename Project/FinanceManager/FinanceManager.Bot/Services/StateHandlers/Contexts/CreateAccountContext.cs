using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.CommandHandlers.Contexts;
public class CreateAccountContext
{
    public string? AccountName { get; set; }
    
    public CurrencyDto? Currency { get; set; }

    public decimal InitialBalance { get; set; }
}

public static class CreateAccountContextExtesion
{
    public static CreateAccountContext GetCreateAccountContext(this UserSession session)
    {
        CreateAccountContext? createAccountContext;
        if (session.ContextData is null)
            throw new StateContextNullException(session.UserState, nameof(createAccountContext));
        createAccountContext = session.ContextData as CreateAccountContext;
        if (createAccountContext is null)
            throw new InvalidCastException(nameof(createAccountContext));
        return createAccountContext;
    }
}