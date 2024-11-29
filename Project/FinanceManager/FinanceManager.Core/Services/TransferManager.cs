using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class TransferManager : BaseManager<Transfer, UpdateTransferDto>, ITransferManager
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

    protected override void Update(Transfer transfer, UpdateTransferDto command)
    {
        transfer.FromAccountId = command.FromAccountId;
        transfer.ToAccountId = command.ToAccountId;
        transfer.Date = command.Date;
        transfer.FromAmount = command.FromAmount;
        transfer.ToAmount = command.ToAmount;
        transfer.Description = command.Description;
    }
}
