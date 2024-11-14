namespace FinanceManager.Core.Services.Abstractions;
public interface IReadOnlyManager<TDto>
{
    public Task<TDto?> GetById(Guid id);

    public Task<TDto[]> GetAll();
}
