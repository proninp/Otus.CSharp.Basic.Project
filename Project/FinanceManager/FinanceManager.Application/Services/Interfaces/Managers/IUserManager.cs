using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IUserManager
{
    Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<UserDto?> GetByTelegramId(long telegramId, CancellationToken cancellationToken);

    Task<UserDto> Create(CreateUserDto command, CancellationToken cancellationToken);

    Task<UserDto> Update(UpdateUserDto command, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}
