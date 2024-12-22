namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IReadOnlyManager<TDto>
{
    public Task<TDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<TDto[]> GetAll(CancellationToken cancellationToken);
}