using System.Net;
using System.Text.RegularExpressions;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Validators;
public sealed class TextSanitizer : ITextSanitizer
{
    private const string DangerousPatternText = @"<script[\s\S]*?>|</script>|<\?|<\s*iframe|<\?xml| echo |win\.ini";
    private readonly Regex _dangerousPattern;

    public TextSanitizer()
    {
        _dangerousPattern = new(DangerousPatternText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    public bool Sanitize(string? value, out string sanitizedValue)
    {
        sanitizedValue = value ?? string.Empty;

        if (string.IsNullOrWhiteSpace(sanitizedValue))
            return true;

        if (_dangerousPattern.IsMatch(sanitizedValue))
            return false;

        sanitizedValue = WebUtility.HtmlEncode(sanitizedValue);

        return true;
    }
}
