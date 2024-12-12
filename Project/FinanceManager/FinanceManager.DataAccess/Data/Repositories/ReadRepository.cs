using System.Linq.Expressions;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.DataAccess.Data.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : IdentityModel
{
    private protected AppDbContext _context;
    private protected DbSet<T> _dbSet;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity;
    }

    public async Task<T> GetByIdOrThrow(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetById(id, cancellationToken);

        if (entity is null)
            throw new ArgumentException($"Запись {typeof(T).Name} с id:'{id}' не была найдена.");
        return entity;
    }

    public Task<TResult[]> Get<TResult>(
        Expression<Func<T, bool>> predicate,
        Func<T, TResult> selector,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet
            .Where(predicate)
            .AsNoTracking();
        if (orderBy is not null)
            query = orderBy(query);

        return query
            .Select(x => selector(x))
            .ToArrayAsync(cancellationToken);
    }
}
