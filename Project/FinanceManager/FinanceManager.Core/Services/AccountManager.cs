using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class AccountManager : BaseManager<Account, AccountDto, CreateAccountDto, UpdateAccountDto>, IAccountManager
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

    protected override void UpdateModel(Account account, UpdateAccountDto command)
    {
        account.Title = command.Title;
        account.Balance = command.Balance;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;
    }

    public async Task UpdateBalance(Guid id, decimal amount, bool isCommit)
    {
        var account = await GetEntityById(id);

        account.Balance += amount;

        if (isCommit) // TODO убираем
            await _unitOfWork.Commit();
    }

    protected override AccountDto GetViewDto(Account model) =>
        model.ToDto();
}
