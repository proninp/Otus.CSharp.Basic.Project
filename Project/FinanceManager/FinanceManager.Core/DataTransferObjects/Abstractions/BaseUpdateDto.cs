using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.DataTransferObjects.Abstractions;
public abstract class BaseUpdateDto<T> : BaseViewDto, IPutModel<T>
    where T : BaseModel
{
    public abstract T ToModel();
}
