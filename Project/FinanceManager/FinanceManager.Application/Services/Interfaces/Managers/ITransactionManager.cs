using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransactionManager
{
    public Task<TransactionDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<TransactionDto[]> Get(Guid userId, CancellationToken cancellationToken);

    public Task<decimal> GetAccountBalance(Guid userId, Guid accountId, CancellationToken cancellationToken);

    public Task<TransactionDto> Create(CreateTransactionDto command, CancellationToken cancellationToken);

    public Task<TransactionDto> Update(UpdateTransactionDto command, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);
}