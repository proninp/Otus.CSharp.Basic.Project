using Telegram.Bot.Types;

namespace FinanceManager.Bot.Models;
public class UserMessage
{
    public int Id { get; set; }

    public long FromId { get; set; }

    public long ChatId { get; set; }

    public string? Text { get; set; }

    public bool IsContainsInlineKeyboard { get; set; }

    public UserMessage() { }

    public UserMessage(int id, long fromId, long chatId, string? text = null, bool isContainsInlineKeyboard = false)
    {
        Id = id;
        FromId = fromId;
        ChatId = chatId;
        Text = text;
        IsContainsInlineKeyboard = isContainsInlineKeyboard;
    }
}

public static class MessageExtension
{
    public static UserMessage ToUserMessage(this Message message)
    {
        ArgumentNullException.ThrowIfNull(message.From, nameof(message.From));

        return new UserMessage(
            message.Id,
            message.From.Id,
            message.Chat.Id,
            message.Text,
            message.ReplyMarkup?.InlineKeyboard is not null
        );
    }
}