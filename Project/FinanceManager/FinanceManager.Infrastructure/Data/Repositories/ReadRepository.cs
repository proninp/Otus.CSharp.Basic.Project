using System.Linq.Expressions;
using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : IdentityModel
{
    protected AppDbContext _context;
    protected readonly DbSet<T> _entitySet;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
        _entitySet = _context.Set<T>();
    }

    // TODO прокидывать CancellationToken во всех async методах
    
    public Task<T?> GetById(Guid id)
    {
        return _entitySet
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<TResult[]> Get<TResult>(
        Expression<Func<T, bool>> predicate,
        Func<T, TResult> selector,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = _entitySet
            .Where(predicate)
            .AsNoTracking();

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return query
            .Select(x => selector(x))
            .ToArrayAsync();
    }
}
