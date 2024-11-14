using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    public void Validate(PutTransactionDto command, Transaction? transaction);
}
