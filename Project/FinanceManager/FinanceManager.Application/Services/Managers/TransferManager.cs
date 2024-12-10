using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class TransferManager : ITransferManager, IEntityProvider<Transfer>
{
    private readonly IRepository<Transfer> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TransferManager(IRepository<Transfer> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransferDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public Task<TransferDto[]> Get(Guid userId)
    {
        return _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public async Task<TransferDto> Create(CreateTransferDto command)
    {
        var transfer = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync();
        return transfer.ToDto();
    }

    public async Task<TransferDto> Update(UpdateTransferDto command)
    {
        var transfer = await GetEntityById(command.Id);

        transfer.FromAccountId = command.FromAccountId;
        transfer.ToAccountId = command.ToAccountId;
        transfer.Date = command.Date;
        transfer.FromAmount = command.FromAmount;
        transfer.ToAmount = command.ToAmount;
        transfer.Description = command.Description;

        _repository.Update(transfer);
        await _unitOfWork.CommitAsync();
        return transfer.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var transfer = await GetEntityById(id);
        _repository.Delete(transfer);
        await _unitOfWork.CommitAsync();
    }

    private async Task<Transfer> GetEntityById(Guid id)
    {
        var entityProvider = (IEntityProvider<Transfer>)this; // TODO Questionable: IEntityProvider
        var transfer = await entityProvider.GetEntityById(_repository, id);
        return transfer;
    }
}
