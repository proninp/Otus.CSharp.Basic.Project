using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransferManager
{
    public Task<TransferDto?> GetById(Guid id);

    public Task<TransferDto[]> Get(Guid userId);

    public Task<TransferDto> Create(CreateTransferDto command);

    public Task<TransferDto> Update(UpdateTransferDto command);

    public Task Delete(Guid id);
}