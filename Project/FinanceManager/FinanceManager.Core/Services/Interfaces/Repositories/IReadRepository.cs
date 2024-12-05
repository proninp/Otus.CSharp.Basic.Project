using System.Linq.Expressions;
using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Services.Abstractions.Repositories;
public interface IReadRepository<T> where T : IdentityModel
{
    Task<T?> GetById(Guid id);

    Task<TResult[]> Get<TResult>(
        Expression<Func<T, bool>> predicate, 
        Func<T, TResult> selector,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy);
}
