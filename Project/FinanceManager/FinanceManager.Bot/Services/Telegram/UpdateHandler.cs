using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram;
public class UpdateHandler : IUpdateHandler
{
    private readonly IUpdateValidator _validator;
    private readonly IBotStateManager _botStateManager;
    private readonly ILogger _logger;

    public UpdateHandler(IUpdateValidator validator, IBotStateManager botStateManager, ILogger logger)
    {
        _validator = validator;
        _botStateManager = botStateManager;
        _logger = logger;
    }

    public async Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        _logger.Information("HandleError: {Exception}", exception);

        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_validator.Validate(update, out var user))
            return;

        await OnUpdate(botClient, update, user, cancellationToken);
    }

    private async Task OnUpdate(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        _logger.Debug($"Receive an update with type: {update.Type}");

        await _botStateManager.HandleUpdateAsync(botClient, update, user, cancellationToken);
    }

        #region Test examples

        //public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    await (update switch
        //    {
        //        { Message: { } message } => OnMessage(botClient, message),
        //        { EditedMessage: { } message } => OnMessage(botClient, message),
        //        { CallbackQuery: { } callbackQuery } => OnCallbackQuery(botClient, callbackQuery),
        //        { InlineQuery: { } inlineQuery } => OnInlineQuery(botClient, inlineQuery),
        //        { ChosenInlineResult: { } chosenInlineResult } => OnChosenInlineResult(botClient, chosenInlineResult),
        //        // UpdateType.ChannelPost:
        //        // UpdateType.EditedChannelPost:
        //        // UpdateType.ShippingQuery:
        //        // UpdateType.PreCheckoutQuery:
        //        _ => UnknownUpdateHandlerAsync(botClient, update)
        //    });
        //}

        //private async Task OnMessage(ITelegramBotClient bot, Message msg)
        //{
        //    _logger.Information("Receive message type: {MessageType}", msg.Type);
        //    if (msg.Text is not { } messageText)
        //        return;

        //    Message sentMessage = await (messageText.Split(' ')[0] switch
        //    {
        //        "/photo" => SendPhoto(bot, msg),
        //        "/inline_buttons" => SendInlineKeyboard(bot, msg),
        //        "/keyboard" => SendReplyKeyboard(bot, msg),
        //        "/remove" => RemoveKeyboard(bot, msg),
        //        "/request" => RequestContactAndLocation(bot, msg),
        //        "/inline_mode" => StartInlineQuery(bot, msg),
        //        "/throw" => FailingHandler(msg),
        //        _ => Usage(bot, msg)
        //    });
        //    _logger.Information("The message was sent with id: {SentMessageId}", sentMessage.Id);
        //}

        //async Task<Message> Usage(ITelegramBotClient bot, Message msg)
        //{
        //    const string usage = """
        //            <b><u>Bot menu</u></b>:
        //            /photo          - send a photo
        //            /inline_buttons - send inline buttons
        //            /keyboard       - send keyboard buttons
        //            /remove         - remove keyboard buttons
        //            /request        - request location or contact
        //            /inline_mode    - send inline-mode results list
        //            /poll           - send a poll
        //            /poll_anonymous - send an anonymous poll
        //            /throw          - what happens if handler fails
        //        """;
        //    return await bot.SendMessage(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        //}

        //async Task<Message> SendPhoto(ITelegramBotClient bot, Message msg)
        //{
        //    await bot.SendChatAction(msg.Chat, ChatAction.UploadPhoto);
        //    await Task.Delay(2000); // simulate a long task
        //    await using var fileStream = new FileStream("Files/bot.gif", FileMode.Open, FileAccess.Read);
        //    return await bot.SendPhoto(msg.Chat, fileStream, caption: "Read https://telegrambots.github.io/book/");
        //}

        //async Task<Message> SendInlineKeyboard(ITelegramBotClient bot, Message msg)
        //{
        //    var inlineMarkup = new InlineKeyboardMarkup()
        //        .AddNewRow("1.1", "1.2", "1.3")
        //        .AddNewRow()
        //            .AddButton("WithCallbackData", "CallbackData")
        //            .AddButton(InlineKeyboardButton.WithUrl("WithUrl", "https://github.com/TelegramBots/Telegram.Bot"));
        //    return await bot.SendMessage(msg.Chat, "Inline buttons:", replyMarkup: inlineMarkup);
        //}

        //async Task<Message> SendReplyKeyboard(ITelegramBotClient bot, Message msg)
        //{
        //    var replyMarkup = new ReplyKeyboardMarkup(true)
        //        .AddNewRow("1.1", "1.2", "1.3")
        //        .AddNewRow().AddButton("2.1").AddButton("2.2");
        //    return await bot.SendMessage(msg.Chat, "Keyboard buttons:", replyMarkup: replyMarkup);
        //}

        //async Task<Message> RemoveKeyboard(ITelegramBotClient bot, Message msg)
        //{
        //    return await bot.SendMessage(msg.Chat, "Removing keyboard", replyMarkup: new ReplyKeyboardRemove());
        //}

        //async Task<Message> RequestContactAndLocation(ITelegramBotClient bot, Message msg)
        //{
        //    var replyMarkup = new ReplyKeyboardMarkup(true)
        //        .AddButton(KeyboardButton.WithRequestLocation("Location"))
        //        .AddButton(KeyboardButton.WithRequestContact("Contact"));
        //    return await bot.SendMessage(msg.Chat, "Who or Where are you?", replyMarkup: replyMarkup);
        //}

        //async Task<Message> StartInlineQuery(ITelegramBotClient bot, Message msg)
        //{
        //    var button = InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Inline Mode");
        //    return await bot.SendMessage(msg.Chat, "Press the button to start Inline Query\n\n" +
        //        "(Make sure you enabled Inline Mode in @BotFather)", replyMarkup: new InlineKeyboardMarkup(button));
        //}

        //static Task<Message> FailingHandler(Message msg)
        //{
        //    throw new NotImplementedException("FailingHandler");
        //}

        //private async Task OnCallbackQuery(ITelegramBotClient bot, CallbackQuery callbackQuery)
        //{
        //    _logger.Information("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);
        //    await bot.AnswerCallbackQuery(callbackQuery.Id, $"Received {callbackQuery.Data}");
        //    await bot.SendMessage(callbackQuery.Message!.Chat, $"Received {callbackQuery.Data}");
        //}

        //private async Task OnInlineQuery(ITelegramBotClient bot, InlineQuery inlineQuery)
        //{
        //    _logger.Information("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

        //    InlineQueryResult[] results = [ // displayed result
        //        new InlineQueryResultArticle("1", "Telegram.Bot", new InputTextMessageContent("hello")),
        //        new InlineQueryResultArticle("2", "is the best", new InputTextMessageContent("world"))
        //    ];
        //    await bot.AnswerInlineQuery(inlineQuery.Id, results, cacheTime: 0, isPersonal: true);
        //}

        //private async Task OnChosenInlineResult(ITelegramBotClient bot, ChosenInlineResult chosenInlineResult)
        //{
        //    _logger.Information("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);
        //    await bot.SendMessage(chosenInlineResult.From.Id, $"You chose result with Id: {chosenInlineResult.ResultId}");
        //}

        //private Task UnknownUpdateHandlerAsync(ITelegramBotClient bot, Update update)
        //{
        //    _logger.Information("Unknown update type: {UpdateType}", update.Type);
        //    return Task.CompletedTask;
        //}

        #endregion
    }
