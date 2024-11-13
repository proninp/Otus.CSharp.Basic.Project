using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface IAccountManager
{
    public Task<AccountDto?> GetById(Guid id);

    public Task<AccountDto[]> Get(Guid userId);

    public Task Put(PutAccountDto command);

    public Task Delete(Guid id);
}
