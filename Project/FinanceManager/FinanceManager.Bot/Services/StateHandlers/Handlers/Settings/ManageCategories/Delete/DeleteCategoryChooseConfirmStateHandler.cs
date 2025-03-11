using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete
{
    public sealed class DeleteCategoryChooseConfirmStateHandler : IStateHandler
    {
        private readonly ICallbackDataProvider _callbackDataProvider;
        private readonly IMessageManager _messageManager;
        private readonly ISessionStateManager _sessionStateManager;

        public DeleteCategoryChooseConfirmStateHandler(
            ICallbackDataProvider callbackDataProvider,
            IMessageManager messageManager,
            ISessionStateManager sessionStateManager)
        {
            _callbackDataProvider = callbackDataProvider;
            _messageManager = messageManager;
            _sessionStateManager = sessionStateManager;
        }

        public async Task<bool> HandleAsync(BotUpdateContext updateContext)
        {
            await _messageManager.DeleteLastMessage(updateContext);

            var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true);
            if (callbackData is null || string.IsNullOrEmpty(callbackData.Data))
                return await _sessionStateManager.Previous(updateContext.Session);

            var isConformText = callbackData.Data;

            if (!bool.TryParse(isConformText, out var isConfirm))
                return await _sessionStateManager.Previous(updateContext.Session);

            var context = updateContext.Session.GetDeleteCategoryContext();
            context.IsConfirm = isConfirm;

            return await _sessionStateManager.Next(updateContext.Session);
        }
    }
}
