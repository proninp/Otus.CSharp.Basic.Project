using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public abstract class TransactionManagerBase : BaseManager<Transaction, TransactionDto, CreateTransactionDto, UpdateTransactionDto>
{
    protected TransactionManagerBase(IRepository<Transaction> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public event Action<CreateTransactionDto>? OnBeforeCreate;
    public event Action<UpdateTransactionDto>? OnBeforeUpdate;

    public override Task<TransactionDto> Create(CreateTransactionDto command)
    {
        if (OnBeforeCreate is not null)
            OnBeforeCreate.Invoke(command);
        return base.Create(command);
    }

    public override Task<TransactionDto> Update(UpdateTransactionDto command)
    {
        if (OnBeforeUpdate is not null)
            OnBeforeUpdate.Invoke(command);
        return base.Update(command);
    }
}
