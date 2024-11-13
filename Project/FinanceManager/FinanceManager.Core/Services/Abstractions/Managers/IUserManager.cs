using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface IUserManager
{
    public Task<UserDto?> GetById(Guid id);

    public Task Put(PutUserDto command);

    public Task Delete(Guid id);
}
