using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ITransferManager
{
    Task<TransferDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<TransferDto[]> GetAsync(Guid userId, CancellationToken cancellationToken);

    Task<TransferDto> CreateAsync(CreateTransferDto command, CancellationToken cancellationToken);

    Task<TransferDto> UpdateAsync(UpdateTransferDto command, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}