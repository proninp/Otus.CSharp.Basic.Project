using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IHistoryMessageTextProvider
{
    string GetMessgaText(IEnumerable<TransactionDto> incomes, IEnumerable<TransactionDto> expenses);
}
