using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Services.Abstractions;
public interface IReadRepository<T> where T : BaseModel
{
    Task<T?> GetById(Guid id);

    Task<TResult[]> Get<TResult>(Func<T, bool> predicate, Func<T, TResult> selector);
}
