namespace FinanceManager.Core.DataTransferObjects.ViewModels;
public sealed class FinanceViewModel
{
    public AccountDto[] Accounts { get; init; }

    public CategoryDto[] Categories { get; init; }

    public TransactionDto[] Transactions { get; init; }

    public TransferDto[] Transfers { get; init; }
}
