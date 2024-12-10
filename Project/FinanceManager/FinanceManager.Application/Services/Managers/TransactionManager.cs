using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class TransactionManager : ITransactionManager, IEntityProvider<Transaction>
{
    private readonly ITransactionValidator _transactionValidator;
    private readonly IRepository<Transaction> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionManager(ITransactionValidator transactionValidator, IRepository<Transaction> repository, IUnitOfWork unitOfWork)
    {
        _transactionValidator = transactionValidator;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public Task<TransactionDto[]> Get(Guid userId)
    {
        return _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public async Task<decimal> GetAccountBalance(Guid userId, Guid accountId)
    {
        return (await _repository.Get(t => t.UserId == userId && t.AccountId == accountId, t => t.ToDto())).Sum(t => t.Amount);
    }

    public async Task<TransactionDto> Create(CreateTransactionDto command)
    {
        _transactionValidator.Validate(command);
        var transaction = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync();
        return transaction.ToDto();
    }

    public async Task<TransactionDto> Update(UpdateTransactionDto command)
    {
        _transactionValidator.Validate(command);
        var transaction = await GetEntityById(command.Id);

        transaction.AccountId = command.AccountId;
        transaction.CategoryId = command.CategoryId;
        transaction.Date = command.Date;
        transaction.Amount = command.TransactionType is TransactionType.Income ?
            Math.Abs(command.Amount) : -Math.Abs(command.Amount);
        transaction.Description = command.Description;

        _repository.Update(transaction);
        await _unitOfWork.CommitAsync();
        return transaction.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var transaction = await GetEntityById(id);
        _repository.Delete(transaction);
        await _unitOfWork.CommitAsync();
    }

    private async Task<Transaction> GetEntityById(Guid id)
    {
        var entityProvider = (IEntityProvider<Transaction>)this; // TODO Questionable: IEntityProvider
        var transaction = await entityProvider.GetEntityById(_repository, id);
        return transaction;
    }
}