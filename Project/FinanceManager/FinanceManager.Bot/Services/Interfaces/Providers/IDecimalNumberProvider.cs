namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IDecimalNumberProvider
{
    bool Provide(string? textValue, out decimal value);
}