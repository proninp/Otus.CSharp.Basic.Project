using FinanceManager.Bot.Services.Abstractions;
using Serilog;
using Telegram.Bot;

namespace FinanceManager.Bot.Services;
public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger logger)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);