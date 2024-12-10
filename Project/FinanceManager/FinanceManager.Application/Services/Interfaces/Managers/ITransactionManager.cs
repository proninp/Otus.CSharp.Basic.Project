using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransactionManager
{
    public Task<TransactionDto?> GetById(Guid id);

    public Task<TransactionDto[]> Get(Guid userId);

    public Task<decimal> GetAccountBalance(Guid userId, Guid accountId);

    public Task<TransactionDto> Create(CreateTransactionDto command);

    public Task<TransactionDto> Update(UpdateTransactionDto command);

    public Task Delete(Guid id);
}