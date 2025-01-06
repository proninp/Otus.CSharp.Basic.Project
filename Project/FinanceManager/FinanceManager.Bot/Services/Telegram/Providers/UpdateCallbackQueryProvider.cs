using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces.Providers;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram.Providers;
internal class UpdateCallbackQueryProvider : IUpdateCallbackQueryProvider
{
    public bool GetCallbackQuery(Update update, [NotNullWhen(true)] out CallbackQuery? callbackQuery)
    {
        callbackQuery = update.CallbackQuery;
        return callbackQuery is not null;
    }
}