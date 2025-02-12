using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransactionManager
{
    Task<TransactionDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<TransactionDto[]> Get(Guid userId, CancellationToken cancellationToken, int pageIndex = 0, int pageSize = 20);

    Task<BigDecimal> GetAccountBalance(Guid userId, Guid accountId, CancellationToken cancellationToken);

    Task<bool> Exists(Guid userId, Guid accountId, CancellationToken cancellationToken);

    Task<long> GetCount(Guid userId, Guid accountId, CancellationToken cancellationToken);

    Task<TransactionDto> Create(CreateTransactionDto command, CancellationToken cancellationToken);

    Task<TransactionDto> Update(UpdateTransactionDto command, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}