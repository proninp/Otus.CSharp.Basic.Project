using System.ComponentModel;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public enum TransactionType
{
    [Description("Expense")]
    Expense,
    [Description("Income")]
    Income
}