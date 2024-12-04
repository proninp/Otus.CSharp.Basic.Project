using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Interfaces.Managers;
public interface IEntityProvider<T>
    where T : IdentityModel
{
    public async Task<T> GetEntityById(IRepository<T> repository, Guid id)
    {
        var entry = await repository.GetById(id);
        if (entry is null)
            throw new ArgumentException($"Запись {typeof(T).Name} с id:'{id}' не была найдена.");
        return entry;
    }
}