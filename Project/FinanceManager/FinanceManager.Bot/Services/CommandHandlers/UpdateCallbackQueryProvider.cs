using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers;
internal class UpdateCallbackQueryProvider : IUpdateCallbackQueryProvider
{
    public bool GetCallbackQuery(Update update, [NotNullWhen(true)] out CallbackQuery? callbackQuery)
    {
        callbackQuery = update.CallbackQuery;
        return callbackQuery is not null;
    }
}