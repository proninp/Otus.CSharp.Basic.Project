﻿using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers;
public class StartStateHandler : ICommandStateHandler
{
    public Task HandleStateAsync(UserSession userSession, Message message)
    {
        throw new NotImplementedException();
    }
}
