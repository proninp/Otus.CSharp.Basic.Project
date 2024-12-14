using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionManager : IUserSessionManager
{
    private readonly IUserManager _userManager;

    public UserSessionManager(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserSession> InstantiateSession(User from)
    {
        var userDto = await _userManager.GetByTelegramId(from.Id);
        if (userDto is null)
        {
            var userCommand = new CreateUserDto
            {
                TelegramId = from.Id,
                Username = from.Username,
                Firstname = from.FirstName,
                Lastname = from.LastName
            };
            userDto = await _userManager.Create(userCommand);
        }
        return userDto.ToUserSession();
    }
}
