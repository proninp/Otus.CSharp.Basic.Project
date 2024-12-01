using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface ITransactionManager
{
    public Task<TransactionDto?> GetById(Guid id);

    public Task<TransactionDto[]> Get(Guid userId);

    public Task<decimal> GetAccountBalance(Guid userId, Guid accountId);

    public Task<TransactionDto> Create(CreateTransactionDto command);

    public Task<TransactionDto> Update(UpdateTransactionDto command);

    public Task Delete(Guid id);
}
