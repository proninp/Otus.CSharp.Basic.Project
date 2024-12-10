using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.DataTransferObjects.Abstractions;
public interface ITransactionableCommand
{
    Guid AccountId { get; set; }

    TransactionType TransactionType { get; set; }

    decimal Amount { get; set; }
}
