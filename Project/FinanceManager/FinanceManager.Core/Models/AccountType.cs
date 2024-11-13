using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public class AccountType : BaseModel
{
    public string Name { get; init; }

    public AccountType(string name)
    {
        Name = name;
    }
}
