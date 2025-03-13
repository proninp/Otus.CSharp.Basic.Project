using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Enums;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class TransferManager : ITransferManager
{
    private readonly IRepository<Transfer> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TransferManager(IRepository<Transfer> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransferDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetByIdAsync(id, cancellationToken: cancellationToken))?.ToDto();
    }

    public Task<TransferDto[]> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(
            t => t.ToDto(),
            t => t.UserId == userId,
            cancellationToken: cancellationToken);
    }

    public async Task<TransferDto> CreateAsync(CreateTransferDto command, CancellationToken cancellationToken)
    {
        var transfer = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return transfer.ToDto();
    }

    public async Task<TransferDto> UpdateAsync(UpdateTransferDto command, CancellationToken cancellationToken)
    {
        var transfer = await _repository.GetByIdOrThrowAsync(command.Id, cancellationToken: cancellationToken);

        transfer.FromAccountId = command.FromAccountId;
        transfer.ToAccountId = command.ToAccountId;
        transfer.Date = command.Date;
        transfer.FromAmount = command.FromAmount;
        transfer.ToAmount = command.ToAmount;
        transfer.Description = command.Description;

        _repository.Update(transfer);
        await _unitOfWork.CommitAsync(cancellationToken);
        return transfer.ToDto();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var transfer = await _repository.GetByIdOrThrowAsync(id, trackingType: TrackingType.Tracking, cancellationToken: cancellationToken);
        _repository.Delete(transfer);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}