using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.DataTransferObjects.Abstractions;
public interface IPutModel<T> where T : BaseModel
{
    T ToModel();
}
