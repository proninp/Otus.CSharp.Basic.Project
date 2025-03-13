using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class CurrencyManager : ICurrencyManager
{
    private readonly IRepository<Currency> _repository;

    public CurrencyManager(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetByIdAsync(id, cancellationToken: cancellationToken))?.ToDto();
    }

    public async Task<CurrencyDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(
            c => c.ToDto(),
            orderBy: q => q.OrderBy(c => c.CurrencyCode),
            cancellationToken: cancellationToken);
    }
}
