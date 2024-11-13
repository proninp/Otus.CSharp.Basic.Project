using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class AccountManager : BaseManager<Account, PutAccountDto>, IAccountManager
{
    public AccountManager(IRepository<Account> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<AccountDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<AccountDto[]> Get(Guid userId)
    {
        return await _repository.Get(a => a.UserId == userId, a => a.ToDto());
    }   

    protected override void Update(Account account, PutAccountDto command)
    {
        account.Title = command.Title;
        account.Balance = command.Balance;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;
    }
}
