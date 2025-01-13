using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class ChooseAccountNameStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageSenderManager _messageSender;

    public ChooseAccountNameStateHandler(
        IAccountManager accountManager, IUpdateMessageProvider messageProvider, IMessageSenderManager messageSender)
    {
        _accountManager = accountManager;
        _messageProvider = messageProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
        {
            updateContext.Session.Wait();
            return;
        }

        var accountTitle = message.Text;
        if (string.IsNullOrWhiteSpace(accountTitle) || accountTitle.Length == 0)
        {
            await _messageSender.SendErrorMessage(updateContext,
                "The account name must contain at least one non-whitespace character.");

            updateContext.Session.Wait();
            return;
        }
        if (!char.IsLetterOrDigit(accountTitle[0]))
        {
            await _messageSender.SendErrorMessage(updateContext,
                "The account name must start with a number or letter. Enter a different account name.");

            updateContext.Session.Wait();
            return;
        }
        var existingAccount = await _accountManager.GetByName(updateContext.Session.Id, accountTitle, false, updateContext.CancellationToken);
        if (existingAccount is not null)
        {
            await _messageSender.SendErrorMessage(updateContext,
                "An account with that name already exists. Enter a different name.");

            updateContext.Session.Wait();
            return;
        }

        var context = new CreateAccountContext { AccountName = accountTitle };
        updateContext.Session.SetData(context);
        updateContext.Session.Continue(WorkflowState.SendCurrencies);
    }
}
