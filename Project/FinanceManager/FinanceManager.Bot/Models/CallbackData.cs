using System.Text;

namespace FinanceManager.Bot.Models;
public class CallbackData
{
    private const char Separator = ' ';

    public string UserSessionId { get; set; }

    public string Data { get; set; } = string.Empty;

    public CallbackData(string sessionId, string data)
    {
        UserSessionId = sessionId;
        Data = data;
    }

    public CallbackData(BotUpdateContext updateContext, string data)
        : this(updateContext.Session.CallbackSessionId, data)
    {
    }

    public string ToData()
    {
        var dataBuilder = new StringBuilder();
        dataBuilder
            .Append(UserSessionId)
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

        foreach (var part in parts)
            if (string.IsNullOrEmpty(part))
                return null;

        return new CallbackData(parts[0], parts[1]);
    }
}