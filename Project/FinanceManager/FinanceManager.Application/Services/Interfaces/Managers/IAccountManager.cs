using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IAccountManager
{
    public Task<AccountDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<AccountDto?> GetDefault(Guid userId, CancellationToken cancellationToken);

    public Task<AccountDto?> GetByName(Guid userId, string accountName, bool isIncludeBalance, CancellationToken cancellationToken);

    public Task<AccountDto[]> Get(Guid userId, CancellationToken cancellationToken);

    public Task<AccountDto> Create(CreateAccountDto command, CancellationToken cancellationToken);

    public Task<AccountDto> Update(UpdateAccountDto command, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);

    public Task<decimal> GetBalance(AccountDto viewModel, CancellationToken cancellationToken);
}
