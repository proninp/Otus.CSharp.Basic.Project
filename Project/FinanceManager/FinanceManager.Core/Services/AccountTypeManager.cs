using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;

namespace FinanceManager.Core.Services;
public class AccountTypeManager
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

    public async Task<AccountTypeDto[]> GetAccountTypes()
    {
        return await _repository.Get(_ => true, a => a.ToDto());
    }
}
