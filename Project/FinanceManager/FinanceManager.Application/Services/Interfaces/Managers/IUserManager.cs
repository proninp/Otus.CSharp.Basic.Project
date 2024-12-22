using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface IUserManager
{
    public Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<UserDto?> GetByTelegramId(long telegramId, CancellationToken cancellationToken);

    public Task<UserDto> Create(CreateUserDto command, CancellationToken cancellationToken);

    public Task<UserDto> Update(UpdateUserDto command, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);
}
