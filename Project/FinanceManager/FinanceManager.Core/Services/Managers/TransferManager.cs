using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Managers;
public class TransferManager : BaseManager<Transfer, TransferDto, CreateTransferDto, UpdateTransferDto>, ITransferManager
{
    public TransferManager(IRepository<Transfer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<TransferDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<TransferDto[]> Get(Guid userId)
    {
        return await _repository.Get(t => t.UserId == userId, t => t.ToDto());
    }

    protected override void UpdateModel(Transfer model, UpdateTransferDto command)
    {
        model.FromAccountId = command.FromAccountId;
        model.ToAccountId = command.ToAccountId;
        model.Date = command.Date;
        model.FromAmount = command.FromAmount;
        model.ToAmount = command.ToAmount;
        model.Description = command.Description;
    }

    protected override TransferDto GetViewDto(Transfer model) =>
        model.ToDto();
}
