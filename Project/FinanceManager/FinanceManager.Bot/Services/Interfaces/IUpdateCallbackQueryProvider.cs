using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUpdateCallbackQueryProvider
{
    bool GetCallbackQuery(Update update, [NotNullWhen(true)] out CallbackQuery? callbackQuery);
}