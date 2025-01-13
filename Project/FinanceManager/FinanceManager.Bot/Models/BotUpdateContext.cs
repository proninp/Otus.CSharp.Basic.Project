using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Models;
public class BotUpdateContext
{
    public UserSession Session { get; init; }

    public ITelegramBotClient BotClient { get; init; }

    public Update Update { get; init; }

    public Chat Chat { get; init; }

    public CancellationToken CancellationToken { get; init; }

    public Message? LastMessage { get; set; }

    public BotUpdateContext(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken)
    {
        Session = session;
        BotClient = botClient;
        Update = update;
        Chat = chat;
        CancellationToken = cancellationToken;
    }
}
