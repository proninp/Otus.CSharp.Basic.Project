using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;
using FinanceManager.Core.Services.Interfaces.Managers;

namespace FinanceManager.Core.Services.Managers;
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

    public async Task<TransferDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    public async Task<TransferDto> Create(CreateTransferDto command)
    {
        var model = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return model.ToDto();
    }

    public async Task<TransferDto> Update(UpdateTransferDto command)
    {
        var entityProvider = (IEntityProvider<Transfer>)this; // TODO Questionable: IEntityProvider
        var transfer = await entityProvider.GetEntityById(_repository, command.Id);

        transfer.FromAccountId = command.FromAccountId;
        transfer.ToAccountId = command.ToAccountId;
        transfer.Date = command.Date;
        transfer.FromAmount = command.FromAmount;
        transfer.ToAmount = command.ToAmount;
        transfer.Description = command.Description;

        _repository.Update(transfer);
        await _unitOfWork.Commit();
        return transfer.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var entityProvider = (IEntityProvider<Transfer>)this; // TODO Questionable: IEntityProvider
        var transfer = await entityProvider.GetEntityById(_repository, id);
        _repository.Delete(transfer);
        await _unitOfWork.Commit();
    }
}
