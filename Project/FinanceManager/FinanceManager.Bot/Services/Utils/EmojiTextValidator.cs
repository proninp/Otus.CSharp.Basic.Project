using System.Globalization;
using System.Text.RegularExpressions;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.Utils;
public sealed class EmojiTextValidator : IEmojiTextValidator
{
    public bool ValidateSingleEmoji(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        text = text.Trim();

        var enumerator = StringInfo.GetTextElementEnumerator(text);
        int count = 0;

        while (enumerator.MoveNext())
        {
            count++;
            if (count > 1)
                return false;
        }

        return count == 1 && IsEmoji(text);
    }

    private static bool IsEmoji(string textElement)
    {
        if (textElement.Length == 0)
            return false;

        int codepoint = char.ConvertToUtf32(textElement, 0);

        return (codepoint >= 0x1F300 && codepoint <= 0x1FAFF) ||
               (codepoint >= 0x1F600 && codepoint <= 0x1F64F) ||
               (codepoint >= 0x2600 && codepoint <= 0x26FF) ||
               (codepoint >= 0x2700 && codepoint <= 0x27BF) ||
               (codepoint >= 0xFE00 && codepoint <= 0xFE0F) ||
               (codepoint >= 0x1F1E6 && codepoint <= 0x1F1FF);
    }
}
