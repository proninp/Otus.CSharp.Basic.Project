using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Managers;

public class UserManager : BaseManager<User, UserDto, CreateUserDto, UpdateUserDto>, IUserManager
{
    public UserManager(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    protected override UserDto GetViewDto(User model) =>
        model.ToDto();

    protected override void UpdateModel(User user, UpdateUserDto command)
    {
        user.Name = command.Name;
    }
}