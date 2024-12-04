using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Abstractions;
public interface ITransactionableCommand
{
    Guid AccountId { get; set; }

    TransactionType TransactionType { get; set; }

    decimal Amount { get; set; }
}
