using System.Globalization;
using FinanceManager.Bot.Services.Interfaces.Providers;

namespace FinanceManager.Bot.Services.StateHandlers.Providers;
public class DecimalNumberProvider : IDecimalNumberProvider
{
    public bool Provide(string? textValue, out decimal value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(textValue))
            return false;

        textValue = textValue?.Replace(',', '.');

        if (!decimal.TryParse(textValue, NumberStyles.Number, CultureInfo.InvariantCulture, out var decimalValue))
            return false;

        value = Math.Round(decimalValue, 2, MidpointRounding.ToEven);
        return true;
    }
}