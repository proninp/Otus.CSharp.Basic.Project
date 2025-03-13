namespace FinanceManager.Application.Services.Interfaces;
public interface IAccountValidator
{
    Task<bool> ExistsAsync(Guid accountId, CancellationToken cancellationToken);
}
