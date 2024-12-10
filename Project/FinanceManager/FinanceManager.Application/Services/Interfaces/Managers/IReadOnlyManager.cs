namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IReadOnlyManager<TDto>
{
    public Task<TDto?> GetById(Guid id);

    public Task<TDto[]> GetAll();
}