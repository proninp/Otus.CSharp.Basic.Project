using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IAccountManager
{
    Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<AccountDto?> GetDefaultAsync(Guid userId, CancellationToken cancellationToken);

    Task<AccountDto?> GetByNameAsync(Guid userId, string accountName, bool isIncludeBalance, CancellationToken cancellationToken);

    Task<AccountDto[]> GetAsync(Guid userId, CancellationToken cancellationToken);

    Task<AccountDto> CreateAsync(CreateAccountDto command, CancellationToken cancellationToken = default);

    Task<AccountDto> UpdateAsync(UpdateAccountDto command, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<BigDecimal> GetBalanceAsync(AccountDto viewModel, CancellationToken cancellationToken);
}
