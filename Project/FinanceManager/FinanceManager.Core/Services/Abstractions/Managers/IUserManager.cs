using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface IUserManager
{
    public Task<UserDto?> GetById(Guid id);

    public Task Put(UpdateUserDto command);

    public Task Delete(Guid id);
}
