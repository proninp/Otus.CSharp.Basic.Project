using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class AccountManager : BaseManager<Account, AccountDto, CreateAccountDto, UpdateAccountDto>, IAccountManager
{

    private readonly ITransactionManager _transactionManager;
    public AccountManager(ITransactionManager transactionManager, IRepository<Account> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
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

    protected override void UpdateModel(Account account, UpdateAccountDto command)
    {
        account.Title = command.Title;
        account.IsDefault = command.IsDefault;
        account.IsArchived = command.IsArchived;
    }

    public async Task<decimal> GetBalance(AccountDto accountDto)
    {
        return await _transactionManager.GetAccountBalance(accountDto.UserId, accountDto.Id);
    }

    protected override AccountDto GetViewDto(Account model) =>
        model.ToDto();
}
