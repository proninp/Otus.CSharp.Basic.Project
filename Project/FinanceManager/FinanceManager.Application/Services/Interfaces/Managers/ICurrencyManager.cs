using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICurrencyManager
{
    public Task<CurrencyDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<CurrencyDto[]> GetAll(CancellationToken cancellationToken);
}