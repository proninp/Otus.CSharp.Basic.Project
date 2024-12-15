using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Bot.Services.CommandHandlers.Contexts;
public class CreateAccountContext
{
    public string? AccountName { get; set; }
    
    public CurrencyDto? Currency { get; set; }

    public decimal InitialBalance { get; set; }
}
