using FinanceManager.Bot.Services.Telegram.Abstractions;
using Serilog;
using Telegram.Bot;

namespace FinanceManager.Bot.Services.Telegram;
public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger logger)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);