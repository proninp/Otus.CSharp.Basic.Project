using System.Text;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Providers;

namespace FinanceManager.Bot.Services.StateHandlers.Providers;
public class HistoryMessageTextProvider : IHistoryMessageTextProvider
{
    public string GetMessgaText(IEnumerable<TransactionDto> incomes, IEnumerable<TransactionDto> expenses)
    {
        StringBuilder messageBuilder = new StringBuilder();

        if (incomes.Count() > 0)
        {
            var incomesBuilder = new StringBuilder($"{Emoji.Income.GetSymbol()} Incomes:{Environment.NewLine}");
            incomesBuilder.AppendLine();
            incomesBuilder.AppendLine(
                string.Join(Environment.NewLine, incomes.Select(GetTransactionFormattedString)));
            messageBuilder.AppendLine(incomesBuilder.ToString());
            messageBuilder.AppendLine();
        }

        if (expenses.Count() > 0)
        {
            var expensesBuilder = new StringBuilder($"{Emoji.Expense.GetSymbol()} Expenses:{Environment.NewLine}");
            expensesBuilder.AppendLine();
            expensesBuilder.AppendLine(
                string.Join(Environment.NewLine, expenses.Select(GetTransactionFormattedString)));
            messageBuilder.AppendLine(expensesBuilder.ToString());
        }

        return messageBuilder.ToString();
    }

    private string GetTransactionFormattedString(TransactionDto transaction)
    {
        var categoriesSeparator = "  -  ";
        var transactionLine = new StringBuilder($"{Emoji.Clock.GetSymbol()} ");
        transactionLine.Append($"{transaction.Date}: ");
        transactionLine.Append($"<b>{Math.Abs(transaction.Amount)}</b> ");
        transactionLine.Append($"{transaction.Account?.Currency?.CurrencyCode}");
        if (transaction.Category is not null)
        {
            transactionLine.Append(categoriesSeparator);
            transactionLine.Append($"{transaction.Category?.Emoji} {transaction.Category?.Title};");
        }
        else
        {
            if (!string.IsNullOrEmpty(transaction.Description))
            {
                transactionLine.Append(categoriesSeparator);
                transactionLine.Append($"{transaction.Description};");
            }
        }
        return transactionLine.ToString();
    }
}
