using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IUserManager
{
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<UserDto?> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken);

    Task<UserDto> CreateAsync(CreateUserDto command, CancellationToken cancellationToken);

    Task<UserDto> UpdateAsync(UpdateUserDto command, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}