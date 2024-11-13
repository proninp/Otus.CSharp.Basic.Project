using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Repositories;
public class Repository<T> : ReadRepository<T>, IRepository<T> where T : BaseModel
{
    public Repository(AppDbContext context) : base(context)
    {
    }

    public void Add(T item)
    {
        _context.Add(item);
    }

    public void Update(T item)
    {
        _context.Set<T>().Update(item);
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
