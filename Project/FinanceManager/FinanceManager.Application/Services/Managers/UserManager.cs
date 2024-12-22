using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class UserManager : IUserManager
{
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserManager(IRepository<User> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetById(id, cancellationToken))?.ToDto();
    }

    public async Task<UserDto?> GetByTelegramId(long telegramId, CancellationToken cancellationToken)
    {
        return (await _repository.Get(
            u => u.TelegramId == telegramId,
            u => u.ToDto(),
            cancellationToken: cancellationToken))?
            .FirstOrDefault();
    }

    public async Task<UserDto> Create(CreateUserDto command, CancellationToken cancellationToken)
    {
        var user = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return user.ToDto();
    }

    public async Task<UserDto> Update(UpdateUserDto command, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdOrThrow(command.Id, cancellationToken);

        user.Username = command.Username;

        _repository.Update(user);
        await _unitOfWork.CommitAsync(cancellationToken);
        return user.ToDto();
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdOrThrow(id, cancellationToken);
        _repository.Delete(user);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}