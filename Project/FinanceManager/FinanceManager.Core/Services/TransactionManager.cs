using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class TransactionManager : ITransactionManager
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionValidator _transactionValidator;
    protected IRepository<Transaction> _repository;
    protected IUnitOfWork _unitOfWork;

    public TransactionManager(
        IAccountManager accountManager,
        ITransactionValidator transactionValidator,
        IRepository<Transaction> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _accountManager = accountManager;
        _transactionValidator = transactionValidator;
    }

    public async Task<TransactionDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<TransactionDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public virtual async Task Put(PutTransactionDto command)
    {
        _transactionValidator.Validate(command);

        if (command.Id is null)
        {
            _repository.Add(command.ToModel());
        }
        else
        {
            var transaction = await GetEntityById(command.Id.Value);

            transaction.AccountId = command.AccountId;
            transaction.CategoryId = command.CategoryId;
            transaction.Date = command.Date;
            transaction.Amount = command.Amount;
            transaction.Description = command.Description;
        }

        var amount = GetSignedAmount(command.TransactionType, command.Amount);
        await _accountManager.UpdateBalance(command.AccountId, amount, false);

        await _unitOfWork.Commit();
    }

    public async Task Delete(Guid id)
    {
        var entry = await GetEntityById(id);
        _repository.Delete(entry);

        var amount = GetSignedAmount(entry.TransactionType, entry.Amount);
        await _accountManager.UpdateBalance(entry.AccountId, amount, false);

        await _unitOfWork.Commit();
    }

    private async Task<Transaction> GetEntityById(Guid id)
    {
        var entry = await _repository.GetById(id);
        if (entry is null)
            throw new ArgumentException($"Транзакция с id:'{id}' не была найдена.");
        return entry;
    }

    private decimal GetSignedAmount(TransactionType type, decimal amount) =>
        type is TransactionType.Expense ? -amount : amount;

}
