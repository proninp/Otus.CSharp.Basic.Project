using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;
using FinanceManager.Core.Services.Interfaces.Managers;

namespace FinanceManager.Core.Services.Managers;

public sealed class UserManager : IUserManager, IEntityProvider<User>
{
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserManager(IRepository<User> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<UserDto> Create(CreateUserDto command)
    {
        var user = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return user.ToDto();
    }

    public async Task<UserDto> Update(UpdateUserDto command)
    {
        var user = await GetEntityById(command.Id);

        user.Name = command.Name;

        _repository.Update(user);
        await _unitOfWork.Commit();
        return user.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var user = await GetEntityById(id);
        _repository.Delete(user);
        await _unitOfWork.Commit();
    }

    private async Task<User> GetEntityById(Guid id)
    {
        var entityProvider = (IEntityProvider<User>)this; // TODO Questionable: IEntityProvider
        var user = await entityProvider.GetEntityById(_repository, id);
        return user;
    }
}