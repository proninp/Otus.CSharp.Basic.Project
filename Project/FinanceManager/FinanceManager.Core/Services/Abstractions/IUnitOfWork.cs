namespace FinanceManager.Core.Services.Abstractions;
public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken = default);
}
