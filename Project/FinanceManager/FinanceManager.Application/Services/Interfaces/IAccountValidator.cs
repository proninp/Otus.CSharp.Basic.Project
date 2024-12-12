namespace FinanceManager.Application.Services.Interfaces;
public interface IAccountValidator
{
    Task<bool> AccountExists(Guid accountId);
}
