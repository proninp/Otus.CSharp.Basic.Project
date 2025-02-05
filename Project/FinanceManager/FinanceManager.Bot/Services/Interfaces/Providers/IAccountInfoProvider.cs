using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IAccountInfoProvider
{
    Task<string> GetAccountInfoAsync(AccountDto account, CancellationToken cancellationToken);
}