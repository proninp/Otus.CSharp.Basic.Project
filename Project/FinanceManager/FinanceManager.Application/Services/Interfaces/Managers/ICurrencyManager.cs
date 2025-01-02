using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICurrencyManager
{
    Task<CurrencyDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<CurrencyDto[]> GetAll(CancellationToken cancellationToken);
}