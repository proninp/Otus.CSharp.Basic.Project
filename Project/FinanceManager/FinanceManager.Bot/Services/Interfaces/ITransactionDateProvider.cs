namespace FinanceManager.Bot.Services.Interfaces;
public interface ITransactionDateProvider
{
    bool TryParseDate(string? input, out DateOnly date);

    string GetIncorrectDateText();

    string GetSupportedFormatsText();
}
