using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class TransactionManager : TransactionManagerBase, ITransactionManager
{
    private readonly ITransactionValidator _transactionValidator;

    public TransactionManager(
        ITransactionValidator transactionValidator,
        IRepository<Transaction> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
        _transactionValidator = transactionValidator;

        OnBeforeCreate += (command) =>
        {
            _transactionValidator.Validate(command);
        };

        OnBeforeUpdate += (command) =>
        {
            _transactionValidator.Validate(command);
        };
    }

    public async Task<TransactionDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<TransactionDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public async Task<decimal> GetAccountBalance(Guid userId, Guid accountId)
    {
        return (await _repository.Get(t => t.UserId == userId && t.AccountId == accountId, t => t.ToDto())).Sum(t => t.Amount);
    }

    protected override TransactionDto GetViewDto(Transaction model) =>
        model.ToDto();

    protected override void UpdateModel(Transaction model, UpdateTransactionDto command)
    {
        model.AccountId = command.AccountId;
        model.CategoryId = command.CategoryId;
        model.Date = command.Date;
        model.Amount = command.TransactionType is TransactionType.Expense ? 
            -Math.Abs(command.Amount) : Math.Abs(command.Amount);
        model.Description = command.Description;
    }
}