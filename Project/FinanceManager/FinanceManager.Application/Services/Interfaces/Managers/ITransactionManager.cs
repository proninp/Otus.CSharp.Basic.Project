using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransactionManager
{
    Task<TransactionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<TransactionDto[]> GetAsync(Guid userId, CancellationToken cancellationToken, int pageIndex = 0, int pageSize = 20);

    Task<BigDecimal> GetAccountBalanceAsync(Guid userId, Guid accountId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid userId, Guid accountId, CancellationToken cancellationToken);

    Task<long> GetCountAsync(
        Guid userId, Guid accountId = default, Guid categoryId = default, CancellationToken cancellationToken = default);

    Task<TransactionDto> CreateAsync(CreateTransactionDto command, CancellationToken cancellationToken);

    Task<TransactionDto> UpdateAsync(UpdateTransactionDto command, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}