﻿using System.Net;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram;
public sealed class MessageManager : IMessageManager
{
    private readonly ILogger _logger;

    public MessageManager(ILogger logger)
    {
        _logger = logger;
    }

    public async Task SendMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = true)
    {
        var message =
            await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: updateContext.CancellationToken);
        if (isSaveMessage)
            updateContext.Session.LastMessage = message.ToUserMessage();
    }

    public async Task SendErrorMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = false)
    {
        messageText = $"{Enums.Emoji.Error.GetSymbol()} " + messageText;
        await SendMessageAsync(updateContext, messageText, isSaveMessage);
    }

    public async Task SendApproveMessageAsync(BotUpdateContext updateContext, string messageText, bool isSaveMessage = false)
    {
        messageText = $"{Enums.Emoji.Success.GetSymbol()} " + messageText;
        await SendMessageAsync(updateContext, messageText, isSaveMessage);
    }

    public async Task SendInlineKeyboardMessageAsync(
        BotUpdateContext updateContext, string messageText, IReplyMarkup inlineKeyboard)
    {
        var message =
            await updateContext.BotClient.SendMessage(
            updateContext.Chat, messageText,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboard,
            cancellationToken: updateContext.CancellationToken);
        updateContext.Session.LastMessage = message.ToUserMessage();
    }

    public async Task<bool> EditLastMessageAsync(
        BotUpdateContext updateContext, string newMessageText, InlineKeyboardMarkup? inlineKeyboard = default)
    {
        if (updateContext.Session.LastMessage == default)
            return false;

        var messageId = updateContext.Session.LastMessage.Id;

        return await ExecuteWithErrorHandlingAsync(
            async () =>
            {
                var message =
                    await updateContext.BotClient.EditMessageText(
                        chatId: updateContext.Chat.Id,
                        messageId: messageId,
                        text: newMessageText,
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboard,
                        cancellationToken: updateContext.CancellationToken);
                updateContext.Session.LastMessage = message.ToUserMessage();
            },
            "editing",
            messageId
            );
    }

    public async Task<bool> DeleteLastMessageAsync(BotUpdateContext updateContext)
    {
        if (updateContext.Session.LastMessage is null)
            return false;

        var result = await DeleteMessageAsync(updateContext, updateContext.Session.LastMessage.Id);

        updateContext.Session.LastMessage = null;
        return result;
    }

    public async Task<bool> DeleteMessageAsync(BotUpdateContext updateContext, int messageId)
    {
        return await ExecuteWithErrorHandlingAsync(
            async () =>
            {
                await updateContext.BotClient.DeleteMessage(
                    chatId: updateContext.Chat,
                    messageId: messageId,
                    cancellationToken: updateContext.CancellationToken);
            },
            "deleting",
            messageId
            );
    }

    public InlineKeyboardButton CreateInlineButton(BotUpdateContext updateContext, string data, string message)
    {
        var callBackData = new CallbackData(updateContext, data);
        return CreateInlineButton(callBackData, message);
    }

    public InlineKeyboardButton CreateInlineButton(CallbackData data, string message)
    {
        var dataText = data.ToTelegramCallbackData();
        return InlineKeyboardButton.WithCallbackData(message, dataText);
    }

    private async Task<bool> ExecuteWithErrorHandlingAsync(Func<Task> action, string operationName, int messageId)
    {
        try
        {
            await action();
            _logger.Debug($"Message with Id: {messageId} has been deleted.");
            return true;
        }
        catch (ApiRequestException ex) when (ex.ErrorCode == (int)HttpStatusCode.BadRequest)
        {
            _logger.Warning(ex, $"Error during {operationName} message {messageId}. Status code: {ex.ErrorCode}. Message: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Unexpected error during {operationName} message {messageId}.");
            return false;
        }
    }
}