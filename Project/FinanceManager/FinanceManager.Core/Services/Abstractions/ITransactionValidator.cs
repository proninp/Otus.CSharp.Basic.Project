using FinanceManager.Core.DataTransferObjects.Commands;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    public void Validate(PutTransactionDto command);
}
