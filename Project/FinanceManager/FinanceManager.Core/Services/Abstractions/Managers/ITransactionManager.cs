using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface ITransactionManager
{
    public Task<TransactionDto?> GetById(Guid id);

    public Task<TransactionDto[]> Get(Guid userId);

    public Task Put(PutTransactionDto command);

    public Task Delete(Guid id);
}
