using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    void Validate(CreateTransactionDto command);
    void Validate(UpdateTransactionDto command);
}
