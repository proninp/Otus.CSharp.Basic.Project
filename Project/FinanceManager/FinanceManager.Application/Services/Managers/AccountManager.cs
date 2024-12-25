using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class AccountManager : IAccountManager
{
    private readonly IRepository<Account> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionManager _transactionManager;

    public AccountManager(IRepository<Account> repository, IUnitOfWork unitOfWork, ITransactionManager transactionManager)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _transactionManager = transactionManager;
    }

    public async Task<AccountDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var accountDto = (await _repository.GetById(id, cancellationToken))?.ToDto();
        if (accountDto is not null)
            accountDto.Balance = await GetBalance(accountDto, cancellationToken);
        return accountDto;
    }

    public async Task<AccountDto?> GetDefault(Guid userId, CancellationToken cancellationToken)
    {
        return (await _repository.Get(
            a => a.UserId == userId && a.IsDefault,
            a => a.ToDto(),
            cancellationToken: cancellationToken))?
            .FirstOrDefault();
    }

    public async Task<AccountDto?> GetByName(Guid userId, string accountTitle, bool isIncludeBalance, CancellationToken cancellationToken)
    {
        var accountDto = (await _repository.Get(
            a => a.UserId == userId && a.Title == accountTitle,
            a => a.ToDto(),
            cancellationToken: cancellationToken))?
            .FirstOrDefault();
        if (isIncludeBalance && accountDto is not null)
            accountDto.Balance = await GetBalance(accountDto, cancellationToken);
        return accountDto;
    }

    public async Task<AccountDto[]> Get(Guid userId, CancellationToken cancellationToken)
    {
        var accountDtos = await _repository.Get(
            a => a.UserId == userId,
            a => a.ToDto(),
            cancellationToken: cancellationToken);
        foreach (var accountDto in accountDtos)
            accountDto.Balance = await GetBalance(accountDto, cancellationToken);
        return accountDtos;
    }

    public async Task<decimal> GetBalance(AccountDto viewModel, CancellationToken cancellationToken)
    {
        return await _transactionManager.GetAccountBalance(viewModel.UserId, viewModel.Id, cancellationToken);
    }

    public async Task<AccountDto> Create(CreateAccountDto command, CancellationToken cancellationToken)
    {
        var account = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return account.ToDto();
    }

    public async Task<AccountDto> Update(UpdateAccountDto command, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdOrThrow(command.Id, cancellationToken);

        account.Title = command.Title;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;

        _repository.Update(account);
        await _unitOfWork.CommitAsync(cancellationToken);
        return account.ToDto();
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdOrThrow(id, cancellationToken);
        _repository.Delete(account);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}