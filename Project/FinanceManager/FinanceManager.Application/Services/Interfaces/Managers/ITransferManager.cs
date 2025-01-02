using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransferManager
{
    Task<TransferDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<TransferDto[]> Get(Guid userId, CancellationToken cancellationToken);

    Task<TransferDto> Create(CreateTransferDto command, CancellationToken cancellationToken);

    Task<TransferDto> Update(UpdateTransferDto command, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}