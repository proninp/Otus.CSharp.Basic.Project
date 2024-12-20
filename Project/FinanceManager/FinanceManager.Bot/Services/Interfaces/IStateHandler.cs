﻿using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IStateHandler
{
    Task HandleStateAsync(UserSession userSession, Message message, CancellationToken cancellationToken);

    Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken);
}
