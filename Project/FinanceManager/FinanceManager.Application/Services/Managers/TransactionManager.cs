using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class TransactionManager : ITransactionManager
{
    private readonly ITransactionValidator _transactionValidator;
    private readonly IRepository<Transaction> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionManager(
        ITransactionValidator transactionValidator,
        IRepository<Transaction> repository,
        IUnitOfWork unitOfWork)
    {
        _transactionValidator = transactionValidator;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetByIdAsync(id, cancellationToken: cancellationToken))?.ToDto();
    }

    public Task<TransactionDto[]> Get(Guid userId, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(
            t => t.ToDto(),
            t => t.UserId == userId,
            cancellationToken: cancellationToken);
    }

    public async Task<decimal> GetAccountBalance(Guid userId, Guid accountId, CancellationToken cancellationToken)
    {
        return (await _repository.GetAsync(
            t => t.ToDto(),
            t => t.UserId == userId && t.AccountId == accountId,
            cancellationToken: cancellationToken))
            .Sum(t => t.Amount);
    }

    public async Task<TransactionDto> Create(CreateTransactionDto command, CancellationToken cancellationToken)
    {
        await _transactionValidator.Validate(command, cancellationToken);
        var transaction = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return transaction.ToDto();
    }

    public async Task<TransactionDto> Update(UpdateTransactionDto command, CancellationToken cancellationToken)
    {
        await _transactionValidator.Validate(command, cancellationToken);
        var transaction = await _repository.GetByIdOrThrowAsync(command.Id, cancellationToken: cancellationToken);

        transaction.AccountId = command.AccountId;
        transaction.CategoryId = command.CategoryId;
        transaction.Date = command.Date;
        transaction.Amount = command.TransactionType is TransactionType.Income ?
            Math.Abs(command.Amount) : -Math.Abs(command.Amount);
        transaction.Description = command.Description;

        _repository.Update(transaction);
        await _unitOfWork.CommitAsync(cancellationToken);
        return transaction.ToDto();
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdOrThrowAsync(id, cancellationToken: cancellationToken);
        _repository.Delete(transaction);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}