using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class TransactionManager : BaseManager<Transaction, PutTransactionDto>, ITransactionManager
{
    public TransactionManager(
        IAccountManager accountManager,
        IRepository<Transaction> repository,
        IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
        _accountManager = accountManager;
    }

    private readonly IAccountManager _accountManager;

    public async Task<TransactionDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<TransactionDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public override Task Put(PutTransactionDto command)
    {
        var accountTpe = _accountManager.GetById(command.AccountId);
        if (accountTpe is null)
            throw new ArgumentException("Невозможно зарегистрировать транзакцию: не указан счет.");

        return base.Put(command);
    }

    protected override void Update(Transaction transaction, PutTransactionDto command)
    {
        transaction.AccountId = command.AccountId;
        transaction.CategoryId = command.CategoryId;
        transaction.Date = command.Date;
        transaction.Amount = command.Amount;
        transaction.Description = command.Description;
    }
}
