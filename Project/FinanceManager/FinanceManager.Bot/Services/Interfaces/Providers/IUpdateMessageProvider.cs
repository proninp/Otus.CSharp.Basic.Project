using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IUpdateMessageProvider
{
    bool GetMessage(Update update, [NotNullWhen(true)] out Message? message);
}