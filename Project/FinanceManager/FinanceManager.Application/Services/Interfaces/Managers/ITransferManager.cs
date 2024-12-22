using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransferManager
{
    public Task<TransferDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<TransferDto[]> Get(Guid userId, CancellationToken cancellationToken);

    public Task<TransferDto> Create(CreateTransferDto command, CancellationToken cancellationToken);

    public Task<TransferDto> Update(UpdateTransferDto command, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);
}