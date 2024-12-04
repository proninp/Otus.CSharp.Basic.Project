using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions.Interfaces;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Managers;
public sealed class AccountTypeManager : IReadOnlyManager<AccountTypeDto>
{
    private IRepository<AccountType> _repository;

    public AccountTypeManager(IRepository<AccountType> repository)
    {
        _repository = repository;
    }

    public async Task<AccountTypeDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<AccountTypeDto[]> GetAll()
    {
        return await _repository.Get(_ => true, a => a.ToDto());
    }
}