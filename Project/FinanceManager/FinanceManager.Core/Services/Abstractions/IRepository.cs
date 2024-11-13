using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Services.Abstractions;

public interface IRepository<T> : IReadRepository<T> where T : BaseModel
{
    void Add(T item);
    
    void Update(T item);

    void Delete(T item);

    Task Delete(Guid id);
}