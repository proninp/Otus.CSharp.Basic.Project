using AutoMapper;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class CurrencyManager : IReadOnlyManager<CurrencyDto>
{
    private readonly IRepository<Currency> _repository;

    public CurrencyManager(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<CurrencyDto[]> GetAll()
    {
        return await _repository.Get(_ => true, c => c.ToDto());
    }
}
