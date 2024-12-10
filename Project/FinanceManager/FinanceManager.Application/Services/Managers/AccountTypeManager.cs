using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
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