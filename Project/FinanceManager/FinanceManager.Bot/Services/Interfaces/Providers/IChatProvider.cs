using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IChatProvider
{
    bool GetChat(Update update, [NotNullWhen(true)] out Chat? chat);
}
