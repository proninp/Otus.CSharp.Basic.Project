using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface IAccountManager
{
    public Task<AccountDto?> GetById(Guid id);

    public Task<AccountDto[]> Get(Guid userId);

    public Task<AccountDto> Create(CreateAccountDto command);

    public Task<AccountDto> Update(UpdateAccountDto command);

    public Task Delete(Guid id);

    public Task UpdateBalance(Guid id, decimal amount, bool isCommit);
}
