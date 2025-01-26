using System.Linq.Expressions;
using FinanceManager.Core.Enums;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : IdentityModel
{
    private protected AppDbContext _context;
    private protected DbSet<T> _dbSet;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public Task<T?> GetByIdAsync(
        Guid id,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default)
    {
        var query = GetTrackingConfiguredQuery(trackingType);

        if (include != null)
            query = include(query);

        var entity = query
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity;
    }

    public async Task<T> GetByIdOrThrowAsync(
        Guid id,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, include, trackingType, cancellationToken);

        if (entity is null)
            throw new ArgumentException($"Entry {typeof(T).Name} with id:'{id}' was not found in the database.");
        return entity;
    }

    public Task<TResult[]> GetAsync<TResult>(
        Func<T, TResult> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default)
    {
        var query = BuildQueryable(predicate, include, orderBy, trackingType);

        return query
            .Select(x => selector(x))
            .ToArrayAsync(cancellationToken);
    }

    public Task<TResult[]> GetPagedAsync<TResult>(
        Func<T, TResult> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        TrackingType trackingType = TrackingType.NoTracking,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        if (pageIndex < 0)
            throw new ArgumentException("The page index must be greater than or equal to 0");
        if (pageSize < 1)
            throw new ArgumentException("The page size must be greater than or equal to 1");
        int skip = pageIndex * pageSize;

        var query = BuildQueryable(predicate, include, orderBy, trackingType);

        return query
            .Skip(skip)
            .Take(pageSize)
            .Select(x => selector(x))
            .ToArrayAsync(cancellationToken);
    }

    public Task<bool> Exists(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();
        if (predicate is not null)
            return query.AnyAsync(predicate, cancellationToken);
        return query.AnyAsync(cancellationToken);
    }

    public Task<long> Count(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return _dbSet.LongCountAsync(cancellationToken);

        return _dbSet.LongCountAsync(predicate, cancellationToken);
    }

    private IQueryable<T> BuildQueryable(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        TrackingType trackingType = TrackingType.NoTracking)
    {
        var query = GetTrackingConfiguredQuery(trackingType);

        if (include is not null)
            query = include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        return orderBy is null ? query : orderBy(query);
    }

    private IQueryable<T> GetTrackingConfiguredQuery(TrackingType trackingType) =>
        trackingType switch
        {
            TrackingType.NoTracking => _dbSet.AsNoTracking(),
            TrackingType.NoTrackingWithIdentityResolution => _dbSet.AsNoTrackingWithIdentityResolution(),
            TrackingType.Tracking => _dbSet,
            _ => throw new ArgumentOutOfRangeException(nameof(trackingType), trackingType, null)
        };
}
