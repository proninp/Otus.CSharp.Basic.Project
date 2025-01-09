namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface ITransactionDateProvider
{
    bool TryParseDate(string? input, out DateOnly date);

    string GetIncorrectDateText();
}
