using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Models;
public class UpdateHandlerCommand
{
    public UserSession Session { get; set; }

    public ITelegramBotClient BotClient { get; set; }

    public Update Update { get; set; }

    public Chat Chat { get; set; }

    public CancellationToken CancellationToken { get; set; }

    public UpdateHandlerCommand(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken)
    {
        Session = session;
        BotClient = botClient;
        Update = update;
        Chat = chat;
        CancellationToken = cancellationToken;
    }
}
