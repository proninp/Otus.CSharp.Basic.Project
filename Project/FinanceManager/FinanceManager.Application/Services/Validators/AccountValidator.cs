using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Validators;
public class AccountValidator : IAccountValidator
{
    private readonly IRepository<Account> _repository;

    public AccountValidator(IRepository<Account> repository)
    {
        _repository = repository;
    }

    public async Task<bool> AccountExists(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _repository.GetById(accountId, cancellationToken);
        return account is not null;
    }
}
