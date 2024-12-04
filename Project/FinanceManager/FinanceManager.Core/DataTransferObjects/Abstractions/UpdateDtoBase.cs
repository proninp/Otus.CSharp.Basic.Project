using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.DataTransferObjects.Abstractions;
public abstract class UpdateDtoBase<T> : IdentityDtoBase, IPutModel<T>
    where T : IdentityModel
{
    public abstract T ToModel();
}
