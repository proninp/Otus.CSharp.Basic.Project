using FinanceManager.Core.DataTransferObjects.Commands.Update;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    public void Validate(UpdateTransactionDto command);
}
