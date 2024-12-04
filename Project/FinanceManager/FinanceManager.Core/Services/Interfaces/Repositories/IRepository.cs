using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Services.Abstractions.Repositories;

public interface IRepository<T> : IReadRepository<T> where T : IdentityModel
{
    T Add(T item);

    T Update(T item);

    void Delete(T item);

    Task Delete(Guid id);
}