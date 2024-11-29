using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface ITransferManager
{
    public Task<TransferDto?> GetById(Guid id);

    public Task<TransferDto[]> Get(Guid userId);

    public Task Put(UpdateTransferDto command);

    public Task Delete(Guid id);
}
