using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Enums;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Services.Managers;
public sealed class AccountManager : IAccountManager
{
    private readonly IRepository<Account> _repository;
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyManager _currencyManager;

    public AccountManager(
        IRepository<Account> repository,
        IRepository<Transaction> transactionRepository,
        IUnitOfWork unitOfWork,
        ICurrencyManager currencyManager
        )
    {
        _repository = repository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _currencyManager = currencyManager;
    }

    public async Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var accountDto = (await _repository.GetByIdAsync(
            id, include: q => q.Include(a => a.Currency), cancellationToken: cancellationToken))?.ToDto();
        if (accountDto is not null)
            accountDto.Balance = await GetBalanceAsync(accountDto, cancellationToken);
        return accountDto;
    }

    public async Task<AccountDto?> GetDefaultAsync(Guid userId, CancellationToken cancellationToken)
    {
        return (await _repository.GetAsync(
            a => a.ToDto(),
            a => a.UserId == userId && a.IsDefault,
            q => q.Include(a => a.Currency),
            cancellationToken: cancellationToken))?
            .FirstOrDefault();
    }

    public async Task<AccountDto?> GetByNameAsync(
        Guid userId, string accountTitle, bool isIncludeBalance, CancellationToken cancellationToken)
    {
        var accountDto = (await _repository.GetAsync(
            a => a.ToDto(),
            a => a.UserId == userId && a.Title == accountTitle,
            q => q.Include(a => a.Currency),
            cancellationToken: cancellationToken))?
            .FirstOrDefault();
        if (isIncludeBalance && accountDto is not null)
            accountDto.Balance = await GetBalanceAsync(accountDto, cancellationToken);
        return accountDto;
    }

    public async Task<AccountDto[]> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        var accountDtos = await _repository.GetAsync(
            a => a.ToDto(),
            a => a.UserId == userId,
            q => q.Include(a => a.Currency),
            cancellationToken: cancellationToken);
        foreach (var accountDto in accountDtos)
            accountDto.Balance = await GetBalanceAsync(accountDto, cancellationToken);
        return accountDtos;
    }

    public async Task<BigDecimal> GetBalanceAsync(AccountDto viewModel, CancellationToken cancellationToken)
    {
        var balance = (await _transactionRepository.GetAsync(
            t => t.ToDto(),
            t => t.UserId == viewModel.UserId && t.AccountId == viewModel.Id,
            cancellationToken: cancellationToken))
            .Aggregate(BigDecimal.Zero, (totalAmount, t) => totalAmount + t.Amount);
        return BigDecimal.Round(balance, 2);
    }

    public async Task<bool> ExistsAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(accountId, cancellationToken: cancellationToken);
        return account is not null;
    }

    public async Task<AccountDto> CreateAsync(CreateAccountDto command, CancellationToken cancellationToken = default)
    {
        var currency = await _currencyManager.GetByIdAsync(command.CurrencyId, cancellationToken);
        if (currency is null)
            throw new InvalidOperationException($"Currency with ID {command.CurrencyId} was not found in the database.");

        var account = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);

        var accountDto = account.ToDto();
        accountDto.Currency = currency;

        return accountDto;
    }

    public async Task<AccountDto> UpdateAsync(UpdateAccountDto command, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdOrThrowAsync(command.Id, cancellationToken: cancellationToken);

        account.Title = command.Title;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;

        _repository.Update(account);
        await _unitOfWork.CommitAsync(cancellationToken);
        return account.ToDto();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdOrThrowAsync(id, trackingType: TrackingType.Tracking, cancellationToken: cancellationToken);
        _repository.Delete(account);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}