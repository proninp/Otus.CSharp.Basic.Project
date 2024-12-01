using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.Services.Abstractions;
public interface ITransactionValidator
{
    public void Validate(IPutModel<Transaction> command);
}
