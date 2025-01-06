using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Validators;
public interface IUpdateValidator
{
    bool Validate(Update update, [NotNullWhen(true)] out User? user);
}
