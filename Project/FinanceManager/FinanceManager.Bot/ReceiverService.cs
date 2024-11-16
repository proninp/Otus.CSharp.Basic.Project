using FinanceManager.Bot.Abstractions;
using Serilog;
using Telegram.Bot;

namespace FinanceManager.Bot;
public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger logger)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);