using System.Linq.Expressions;
using FinanceManager.Core.Enums;
using FinanceManager.Core.Models.Abstractions;
using Microsoft.EntityFrameworkCore.Query;

namespace FinanceManager.Core.Interfaces.Repositories;
public interface IReadRepository<T> where T : IdentityModel
{
    Task<T?> GetByIdAsync(
        Guid id,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default);

    Task<T> GetByIdOrThrowAsync(
        Guid id,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default);

    Task<TResult[]> GetAsync<TResult>(
        Func<T, TResult> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        TrackingType trackingType = TrackingType.NoTracking,
        CancellationToken cancellationToken = default);

    Task<TResult[]> GetPagedAsync<TResult>(
        Func<T, TResult> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        TrackingType trackingType = TrackingType.NoTracking,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    
    Task<bool> Exists(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}