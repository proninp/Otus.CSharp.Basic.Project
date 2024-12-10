using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.Services.Interfaces;
public interface ITransactionValidator
{
    void Validate(ITransactionableCommand transactionCommand);
}