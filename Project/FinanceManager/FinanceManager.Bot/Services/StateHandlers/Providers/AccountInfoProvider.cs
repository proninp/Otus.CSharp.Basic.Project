using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Providers;
using System.Text;

namespace FinanceManager.Bot.Services.StateHandlers.Providers;
public class AccountInfoProvider : IAccountInfoProvider
{
    private readonly IAccountManager _accountManager;

    public AccountInfoProvider(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task<string> GetAccountInfoAsync(AccountDto account, CancellationToken cancellationToken)
    {
        var balance = await _accountManager.GetBalanceAsync(account, cancellationToken);

        var messageBuilder = new StringBuilder($"{Emoji.IncomeAmount.GetSymbol()} Account: {account.Title}");
        messageBuilder.AppendLine();
        messageBuilder.Append($"{Emoji.Income.GetSymbol()} Balance: {balance}");
        if (account.Currency is not null)
            messageBuilder.Append($" {account.Currency.CurrencyCode} {account.Currency.Emoji}");
        messageBuilder.AppendLine();
        messageBuilder.Append("Choose an action:");

        return messageBuilder.ToString();
    }
}