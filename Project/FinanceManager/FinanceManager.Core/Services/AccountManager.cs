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

    public async Task UpdateBalance(PutTransactionDto command, bool isCommit)
    {
        var account = await _repository.GetById(command.AccountId);
        if (account is null)
            throw new ArgumentException("В транзакции не указан счет.");

        if (command.TransactionType is TransactionType.Income)
            account.Balance += command.Amount;
        else if (command.TransactionType is TransactionType.Expense)
            account.Balance -= command.Amount;

        if (isCommit)
            await _unitOfWork.Commit();
    }
}
