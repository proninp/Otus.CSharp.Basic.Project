using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services;
public sealed class FinanceService(
    AccountManager accountManager,
    CategoryManager categoryManager,
    TransactionManager transactionManager,
    TransferManager transferManager)
{
    public async Task<FinanceViewModel> GetUserFinanceData(Guid userId)
    {
        var accounts = await accountManager.GetAccounts(userId);
        var categories = await categoryManager.GetCategories(userId);
        var transactions = await transactionManager.GetTransactions(userId);
        var transfers = await transferManager.GetTransfers(userId);
        

        return new FinanceViewModel
        {
            Accounts = accounts,
            Categories = categories,
            Transactions = transactions,
            Transfers = transfers
        };
    }
}
