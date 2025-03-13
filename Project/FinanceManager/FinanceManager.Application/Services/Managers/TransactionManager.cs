using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Enums;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<TransactionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetByIdAsync(id, cancellationToken: cancellationToken))?.ToDto();
    }

    public Task<TransactionDto[]> GetAsync(Guid userId, CancellationToken cancellationToken, int pageIndex = 0, int pageSize = 20)
    {
        return _repository.GetPagedAsync(
            t => t.ToDto(),
            t => t.UserId == userId,
            include: q => q
                .Include(t => t.Category)
                .Include(t => t.Account)
                .ThenInclude(a => a.Currency),
            orderBy: q => q.OrderByDescending(t => t.Date),
            pageIndex: pageIndex,
            pageSize: pageSize,
            cancellationToken: cancellationToken);
    }

    public async Task<BigDecimal> GetAccountBalanceAsync(Guid userId, Guid accountId, CancellationToken cancellationToken)
    {
        return (await _repository.GetAsync(
            t => t.ToDto(),
            t => t.UserId == userId && t.AccountId == accountId,
            cancellationToken: cancellationToken))
            .Aggregate(BigDecimal.Zero, (totalAmount, t) => totalAmount + t.Amount);
    }

    public Task<bool> ExistsAsync(Guid userId, Guid accountId, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(t => t.UserId == userId && t.AccountId == accountId, cancellationToken);
    }

    public Task<long> GetCountAsync(
        Guid userId, Guid accountId = default, Guid categoryId = default, CancellationToken cancellationToken = default)
    {
        Expression<Func<Transaction, bool>> predicate = t => t.UserId == userId && 
            (accountId == default || t.AccountId == accountId) &&
            (categoryId == default || t.CategoryId == categoryId);
        return _repository.CountAsync(predicate, cancellationToken);
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto command, CancellationToken cancellationToken)
    {
        await _transactionValidator.ValidateAsync(command, cancellationToken);
        var transaction = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return transaction.ToDto();
    }

    public async Task<TransactionDto> UpdateAsync(UpdateTransactionDto command, CancellationToken cancellationToken)
    {
        await _transactionValidator.ValidateAsync(command, cancellationToken);
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

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdOrThrowAsync(id, trackingType: TrackingType.Tracking, cancellationToken: cancellationToken);
        _repository.Delete(transaction);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}