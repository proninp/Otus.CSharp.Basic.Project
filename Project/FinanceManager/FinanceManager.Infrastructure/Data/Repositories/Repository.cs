using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public sealed class Repository<T> : ReadRepository<T>, IRepository<T> where T : IdentityModel
{
    public Repository(AppDbContext context) : base(context) { }

    public T Add(T item)
    {
        var addResult = _context.Add(item);
        return addResult.Entity;
    }

    public T Update(T item)
    {
        var updateResult = _context.Update(item);
        return updateResult.Entity;
    }

    public void Delete(T item)
    {
        if (_context.Entry(item).State == EntityState.Detached)
            _dbSet.Attach(item);
        _context.Remove(item);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (item is not null)
            Delete(item);
    }
}