using System.Globalization;
using System.Text;
using FinanceManager.Bot.Services.Interfaces.Providers;

namespace FinanceManager.Bot.Services.StateHandlers.Providers;
public class TransactionDateProvider : ITransactionDateProvider
{
    private static readonly char[] _separators = ['-', ' ', '.', '/'];

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

        if (GetDateFromDateParts(input, ref date))
            return true;

        string[] formats = GetDateFormats();

        foreach (var format in formats)
            if (DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return true;
        return false;
    }

    public string GetIncorrectDateText()
    {
        var delimitersText = string.Join(", ", _separators.Select(c => $"<code>{c}</code>"));

        var messageBuilder = new StringBuilder();
        messageBuilder
            .AppendLine($"<b>An incorrect date has been entered.</b>")
            .AppendLine("You can use the following delimiters to enter the date:")
            .AppendLine(delimitersText)
            .AppendLine()
            .AppendLine("You can either enter <code>T</code> for today's date or <code>Y</code> for yesterday's date.")
            .Append("Alternatively, you can specify only the day or the day and month.");

        return messageBuilder.ToString();
    }

    private string[] GetDateFormats() =>
        [
            "yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd", "yyyyMMdd", "yyyy MM dd",
            "dd-MM-yyyy", "dd-MM-yy", "dd/MM/yyyy", "dd/MM/yy", "d.MM.yyyy",
            "d.MM.yy", "dd.MM.yyyy", "dd.MM.yy", "dd MM yyyy", "dd MM yy",
            "ddMMyyyy", "ddMMyy"
        ];

    private bool GetDateFromDateParts(string input, ref DateOnly date)
    {
        var parts = input.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0 || parts.Length > 2)
            return false;
        if (!int.TryParse(parts[0], out var day))
            return false;

        var month = DateTime.Now.Month;

        if (parts.Length > 1)
            if (!int.TryParse(parts[1], out month))
                return false;

        if (!CheckValidDateParts(day, month))
            return false;

        var currentYear = DateTime.Now.Year;
        date = DateOnly.FromDateTime(new DateTime(currentYear, month, day));
        return true;
    }

    private bool CheckValidDateParts(int day, int month)
    {
        if (month < 1 || month > 12)
            return false;
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
        if (day < 1 || day > daysInMonth)
            return false;
        return true;
    }
}