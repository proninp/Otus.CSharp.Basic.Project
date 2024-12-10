using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class AccountManager : IAccountManager, IEntityProvider<Account>
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

    public async Task<AccountDto?> GetById(Guid id)
    {
        var accountDto = (await _repository.GetById(id))?.ToDto();
        if (accountDto is not null)
            accountDto.Balance = await GetBalance(accountDto);
        return accountDto;
    }

    public async Task<AccountDto[]> Get(Guid userId)
    {
        var accountDtos = (await _repository.Get(a => a.UserId == userId, a => a.ToDto()));
        foreach (var accountDto in accountDtos)
            accountDto.Balance = await GetBalance(accountDto);
        return accountDtos;
    }

    public async Task<decimal> GetBalance(AccountDto viewModel)
    {
        return await _transactionManager.GetAccountBalance(viewModel.UserId, viewModel.Id);
    }

    public async Task<AccountDto> Create(CreateAccountDto command)
    {
        var account = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync();
        return account.ToDto();
    }

    public async Task<AccountDto> Update(UpdateAccountDto command)
    {
        var account = await GetEntityById(command.Id);

        account.Title = command.Title;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;

        _repository.Update(account);
        await _unitOfWork.CommitAsync();
        return account.ToDto();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    private async Task<Account> GetEntityById(Guid id)
    {
        var entityProvider = (IEntityProvider<Account>)this; // TODO Questionable: IEntityProvider
        var account = await entityProvider.GetEntityById(_repository, id);
        return account;
    }
}
