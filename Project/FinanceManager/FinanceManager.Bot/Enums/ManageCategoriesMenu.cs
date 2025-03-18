using System.ComponentModel;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename;

namespace FinanceManager.Bot.Enums;

public enum ManageCategoriesMenu
{
    [Description("None")]
    None,
    [Description("Add new")]
    Add,
    [Description("Delete")]
    Delete,
    [Description("Rename")]
    Rename,
}

public static class ManageCategoriesMenuExtension
{
    public static string GetKey(this ManageCategoriesMenu manageCategoryMenu) => manageCategoryMenu switch
    {
        ManageCategoriesMenu.Add => nameof(CreateCategoryStartStateHandler),
        ManageCategoriesMenu.Delete => nameof(DeleteCategoryStartStateHandler),
        ManageCategoriesMenu.Rename => nameof(RenameCategoryUDStateHandler),
        _ => throw new NotImplementedException(manageCategoryMenu.GetDescription())
    };
}
