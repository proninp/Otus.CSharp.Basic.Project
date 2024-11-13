using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : BaseModel
{
    protected AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<TResult[]> Get<TResult>(Func<T, bool> predicate, Func<T, TResult> selector)
    {
        return await _context
            .Set<T>()
            .Where(x => predicate(x))
            .AsNoTracking()
            .Select(x => selector(x))
            .ToArrayAsync();
    }
}
