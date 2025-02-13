namespace FinanceManager.Bot.Services.Interfaces.Validators;
public interface ITextSanitizer
{
    bool Sanitize(string? value, out string sanitizedValue);
}
