using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.MenuHandler;
public class MenuSubStateFactory : IMenuSubStateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MenuSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubStateHandler GetSubStateHandler(UserSubState userSubState) => userSubState switch
    {
        UserSubState.Default => _serviceProvider.GetRequiredService<CreateMenuSubStateHandler>(),
        UserSubState.SelectMenu => _serviceProvider.GetRequiredService<SelectMenuSubStateHandler>(),
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState} in {nameof(MenuSubStateFactory)}")
    };
}
