using FinanceManager.Core.DataTransferObjects.Abstractions;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    void Validate(ITransactionableCommand transactionCommand);
}
