using System.Text;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class CreateMenuStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;

    public CreateMenuStateHandler(IMessageManager messageManager, IAccountManager accountManager)
    {
        _messageManager = messageManager;
        _accountManager = accountManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            updateContext.Session.Reset();
            return;
        }

        var messageText = await GetMessageText(account, updateContext.CancellationToken);
        var inlineKeyboard = CreateInlineKeyboard();

        await _messageManager.SendInlineKeyboardMessage(updateContext, messageText, inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.SelectMenu);
    }

    private async Task<string> GetMessageText(AccountDto account, CancellationToken cancellationToken)
    {
        var balance = await _accountManager.GetBalance(account, cancellationToken);

        var messageBuilder = new StringBuilder($"{Emoji.IncomeAmount.GetSymbol()} Account: {account.Title}");
        messageBuilder.AppendLine();
        messageBuilder.Append($"{Emoji.Income.GetSymbol()} Balance: {balance}");
        if (account.Currency is not null)
            messageBuilder.Append($" {account.Currency.CurrencyCode} {account.Currency.Emoji}");
        messageBuilder.AppendLine();
        messageBuilder.Append("Choose an action:");

        return messageBuilder.ToString();
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Expense.GetSymbol()} {Enums.Menu.Expense.GetDescription()}", $"{Enums.Menu.Expense.GetKey()}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Income.GetSymbol()} {Enums.Menu.Income.GetDescription()}", $"{Enums.Menu.Income.GetKey()}"),
            ],
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.History.GetSymbol()} {Enums.Menu.History.GetDescription()}", $"{Enums.Menu.History.GetKey()}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Settings.GetSymbol()} {Enums.Menu.Settings.GetDescription()}", $"{Enums.Menu.Settings.GetKey()}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
