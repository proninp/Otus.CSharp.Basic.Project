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

    public async Task<CurrencyDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetById(id, cancellationToken))?.ToDto();
    }

    public async Task<CurrencyDto[]> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.Get(_ => true, c => c.ToDto(), cancellationToken: cancellationToken);
    }
}
