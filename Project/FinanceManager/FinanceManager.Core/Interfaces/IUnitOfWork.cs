namespace FinanceManager.Core.Interfaces;
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}