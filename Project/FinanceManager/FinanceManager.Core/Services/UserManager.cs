using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;

public class UserManager : BaseManager<User, PutUserDto>, IUserManager
{
    public UserManager(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    protected override void Update(User user, PutUserDto command)
    {
        user.Name = command.Name;
    }
}