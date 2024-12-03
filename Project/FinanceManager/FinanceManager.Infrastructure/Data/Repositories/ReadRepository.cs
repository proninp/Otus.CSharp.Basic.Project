using System.Linq.Expressions;
using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : BaseModel
{
    protected AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    // TODO прокидывать CancellationToken во всех async методах
    
    public Task<T?> GetById(Guid id)
    {
        return _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<TResult[]> Get<TResult>(Expression<Func<T, bool>> predicate, Func<T, TResult> selector)
    {
        return _context
            .Set<T>()
            .Where(predicate)
            .AsNoTracking()
            .Select(x => selector(x))
            .ToArrayAsync();
    }
}
