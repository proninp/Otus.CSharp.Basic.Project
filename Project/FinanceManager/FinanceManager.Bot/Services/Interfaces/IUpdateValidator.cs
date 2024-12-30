using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUpdateValidator
{
    bool Validate(Update update, [NotNullWhen(true)] out User? user);
}
