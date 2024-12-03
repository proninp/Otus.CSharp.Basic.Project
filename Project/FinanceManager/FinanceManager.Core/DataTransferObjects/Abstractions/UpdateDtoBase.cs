using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.DataTransferObjects.Abstractions;
public abstract class UpdateDtoBase<T> : IdentityDtoBase, IPutModel<T>
    where T : BaseModel
{
    public abstract T ToModel();
}
