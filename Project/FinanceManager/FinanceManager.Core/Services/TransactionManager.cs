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
        ITransactionValidator transactionValidator,
        IRepository<Transaction> repository,
        IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
        _accountManager = accountManager;
        _transactionValidator = transactionValidator;
    }


    private readonly IAccountManager _accountManager;
    private readonly ITransactionValidator _transactionValidator;

    public async Task<TransactionDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<TransactionDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }
    
    protected override void Update(Transaction transaction, PutTransactionDto command)
    {
        transaction.AccountId = command.AccountId;
        transaction.CategoryId = command.CategoryId;
        transaction.Date = command.Date;
        transaction.Amount = command.Amount;
        transaction.Description = command.Description;
    }

    public override async Task Put(PutTransactionDto command)
    {
        Transaction? transaction = null;
        if (command.Id is not null)
            transaction = await _repository.GetById(command.Id.Value);

        _transactionValidator.Validate(command, transaction);
        await _accountManager.UpdateBalance(command, false);
        await base.Put(command);
    }
}
