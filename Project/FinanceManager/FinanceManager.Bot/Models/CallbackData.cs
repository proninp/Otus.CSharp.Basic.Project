using System.Text;

namespace FinanceManager.Bot.Models;
public class CallbackData
{
    private const char Separator = ' ';

    public string CallbackSessionId { get; set; }

    public string Data { get; set; } = string.Empty;

    public CallbackData(string sessionId, string data)
    {
        CallbackSessionId = sessionId;
        Data = data;
    }

    public CallbackData(BotUpdateContext updateContext, string data)
        : this(updateContext.Session.CallbackSessionId, data)
    {
    }

    public string ToTelegramCallbackData()
    {
        var dataBuilder = new StringBuilder();
        dataBuilder
            .Append(CallbackSessionId)
            .Append(Separator)
            .Append(Data);
        return dataBuilder.ToString();
    }


    public static CallbackData? FromRawText(string? textData)
    {
        if (string.IsNullOrEmpty(textData))
            return null;

        var parts = textData.Split(Separator);

        if (parts.Length != 2)
            return null;

        if (parts.Any(p => string.IsNullOrEmpty(p)))
            return null;

        return new CallbackData(parts[0], parts[1]);
    }
}