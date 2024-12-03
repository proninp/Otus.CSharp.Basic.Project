using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;
using FinanceManager.Core.Services.Interfaces.Managers;

namespace FinanceManager.Core.Services.Managers;

public sealed class AccountManager : IAccountManager, IEntityProvider<Account>
{
    private readonly IRepository<Account> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionManager _transactionManager;

    public AccountManager(ITransactionManager transactionManager, IRepository<Account> repository, IUnitOfWork unitOfWork)
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
        var accountDtos = await _repository.Get(a => a.UserId == userId, a => a.ToDto());
        foreach (var accountDto in accountDtos)
            accountDto.Balance = await GetBalance(accountDto);
        return accountDtos;
    }

    public async Task<AccountDto> Create(CreateAccountDto command)
    {
        var model = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return model.ToDto();
    }

    public async Task<AccountDto> Update(UpdateAccountDto command)
    {
        var entityProvider = (IEntityProvider<Account>)this; // TODO Questionable: IEntityProvider
        var account = await entityProvider.GetEntityById(_repository, command.Id);

        account.Title = command.Title;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;

        _repository.Update(account);
        await _unitOfWork.Commit();
        return account.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var entityProvider = (IEntityProvider<Account>)this; // TODO Questionable: IEntityProvider
        var entry = await entityProvider.GetEntityById(_repository, id);
        _repository.Delete(entry);
        await _unitOfWork.Commit();
    }

    public async Task<decimal> GetBalance(AccountDto accountDto)
    {
        return await _transactionManager.GetAccountBalance(accountDto.UserId, accountDto.Id);
    }
}
