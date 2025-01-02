using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IAccountManager
{
    Task<AccountDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<AccountDto?> GetDefault(Guid userId, CancellationToken cancellationToken);

    Task<AccountDto?> GetByName(Guid userId, string accountName, bool isIncludeBalance, CancellationToken cancellationToken);

    Task<AccountDto[]> Get(Guid userId, CancellationToken cancellationToken);

    Task<AccountDto> Create(CreateAccountDto command, CancellationToken cancellationToken = default);

    Task<AccountDto> Update(UpdateAccountDto command, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);

    Task<decimal> GetBalance(AccountDto viewModel, CancellationToken cancellationToken);
}
