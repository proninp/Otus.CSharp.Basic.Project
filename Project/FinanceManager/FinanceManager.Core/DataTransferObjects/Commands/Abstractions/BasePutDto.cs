using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
public abstract class BasePutDto<T> : IPutModel<T>
    where T : BaseModel
{
    public Guid? Id { get; init; }

    public abstract T ToModel();
}
