using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class Repository<T> : ReadRepository<T>, IRepository<T> where T : BaseModel
{
    public Repository(AppDbContext context) : base(context)
    {
    }

    public T Add(T item)
    {
        var addResult = _context.Add(item);
        return addResult.Entity;
    }

    public T Update(T item)
    {
        var updateResult = _context.Set<T>().Update(item);
        return updateResult.Entity;
        
    }

    public void Delete(T item)
    {
        _context.Remove(item);
    }

    public async Task Delete(Guid id)
    {
        var item = await _context.Set<T>().FirstOrDefaultAsync(u => u.Id == id);
        if (item is not null)
        {
            Delete(item);
        }
    }
}
