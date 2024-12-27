using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Interfaces.Repositories;
public interface IRepository<T> : IReadRepository<T> where T : IdentityModel
{
    T Add(T item);

    T Update(T item);

    void Delete(T item);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}