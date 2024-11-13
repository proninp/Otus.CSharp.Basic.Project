using FinanceManager.Core.DataTransferObjects;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;

namespace FinanceManager.Core.Services;

public class CurrencyManager
{
    private IRepository<Currency> _repository;

    public CurrencyManager(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<CurrencyDto[]> GetCurrencies()
    {
        return await _repository.Get(_ => true, c => c.ToDto());
    }
}