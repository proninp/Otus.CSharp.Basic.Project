using System.Linq.Expressions;
using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Interfaces.Repositories;
public interface IReadRepository<T> where T : IdentityModel
{
    Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<TResult[]> Get<TResult>(
        Expression<Func<T, bool>> predicate,
        Func<T, TResult> selector,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default);
}