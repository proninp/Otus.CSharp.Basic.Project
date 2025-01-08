using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
public class ChooseAccountNameSubStateHandler : ISubStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageSenderManager _messageSender;

    public ChooseAccountNameSubStateHandler(
        IAccountManager accountManager, IUpdateMessageProvider messageProvider, IMessageSenderManager messageSender)
    {
        _accountManager = accountManager;
        _messageProvider = messageProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
        {
            session.Wait();
            return;
        }

        var accountTitle = message.Text;
        if (string.IsNullOrWhiteSpace(accountTitle) || accountTitle.Length == 0)
        {
            await _messageSender.SendErrorMessage(botClient, message.Chat,
                "The account name must contain at least one non-whitespace character.", cancellationToken);

            session.Wait();
            return;
        }
        if (!char.IsLetterOrDigit(accountTitle[0]))
        {
            await _messageSender.SendErrorMessage(botClient, message.Chat,
                "The account name must start with a number or letter. Enter a different account name.", cancellationToken);

            session.Wait();
            return;
        }
        var existingAccount = await _accountManager.GetByName(session.Id, accountTitle, false, cancellationToken);
        if (existingAccount is not null)
        {
            await _messageSender.SendErrorMessage(botClient, message.Chat,
                "An account with that name already exists. Enter a different name.", cancellationToken);

            session.Wait();
            return;
        }

        var context = new CreateAccountContext { AccountName = accountTitle };
        session.ContextData = context;
        session.Continue(WorkflowSubState.SendCurrencies);
    }
}
