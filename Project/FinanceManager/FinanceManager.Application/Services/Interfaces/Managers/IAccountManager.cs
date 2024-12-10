using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IAccountManager
{
    public Task<AccountDto?> GetById(Guid id);

    public Task<AccountDto[]> Get(Guid userId);

    public Task<AccountDto> Create(CreateAccountDto command);

    public Task<AccountDto> Update(UpdateAccountDto command);

    public Task Delete(Guid id);

    public Task<decimal> GetBalance(AccountDto viewModel);
}
