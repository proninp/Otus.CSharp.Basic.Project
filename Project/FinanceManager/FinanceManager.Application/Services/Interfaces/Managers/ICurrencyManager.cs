using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICurrencyManager
{
    Task<CurrencyDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<CurrencyDto[]> GetAllAsync(CancellationToken cancellationToken);
}