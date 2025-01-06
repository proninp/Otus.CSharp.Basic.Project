using System.Globalization;
using System.Text;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Providers;

namespace FinanceManager.Bot.Services.Utils;
public class TransactionDateProvider : ITransactionDateProvider
{
    public string GetIncorrectDateText()
    {
        var formats = string.Join(Environment.NewLine, GetDateFormats());

        var messageBuilder = new StringBuilder($"{Emoji.Error.GetSymbol()} ");
        messageBuilder
            .AppendLine($"**An incorrect date has been entered.**")
            .AppendLine(GetSupportedFormatsText())
            .AppendLine("You can enter **'T'** for today's date")
            .Append("or **'Y'** for yesterday's date.");

        return messageBuilder.ToString();
    }

    public string GetSupportedFormatsText()
    {
        var formats = string.Join(Environment.NewLine, GetDateFormats());
        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine("Supported formats:");
        messageBuilder.Append(formats);
        return messageBuilder.ToString();
    }

    public bool TryParseDate(string? input, out DateOnly date)
    {
        date = default;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (input.Equals("T", StringComparison.InvariantCultureIgnoreCase))
        {
            date = DateOnly.FromDateTime(DateTime.Today);
            return true;
        }

        if (input.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
        {
            date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
            return true;
        }

        string[] formats = GetDateFormats();

        foreach (var format in formats)
            if (DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return true;
        return false;
    }

    private string[] GetDateFormats() =>
        [
            "yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd", "yyyyMMdd", "yyyy MM dd",
            "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yyyy", "dd/MM/yy", "d.MM.yyyy",
            "d.MM.yy", "dd.MM.yyyy", "dd.MM.yy", "dd MM yyyy", "dd MM yy",
            "ddMMyyyy", "ddMMyy"
        ];
}
