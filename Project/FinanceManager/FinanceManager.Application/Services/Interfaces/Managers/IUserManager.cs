using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IUserManager
{
    public Task<UserDto?> GetById(Guid id);

    public Task<UserDto> Create(CreateUserDto command);

    public Task<UserDto> Update(UpdateUserDto command);

    public Task Delete(Guid id);
}
