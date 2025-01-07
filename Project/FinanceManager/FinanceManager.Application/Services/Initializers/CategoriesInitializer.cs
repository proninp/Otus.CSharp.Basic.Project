using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Enums;

namespace FinanceManager.Application.Services.Initializers;
public class CategoriesInitializer : ICategoriesInitializer
{
    private readonly ICategoryManager _categoryManager;
    private readonly (CategoryType CategoryType, string Title, string Emoji)[] _defaultCategories = new[]
    {
        (CategoryType.Expense, "Car", "🚗"),
        (CategoryType.Expense, "Cafes and Restaurants", "🍽️"),
        (CategoryType.Expense, "Clothing", "🧥"),
        (CategoryType.Income, "Deposit Interest", "💰"),
        (CategoryType.Expense, "Education", "🎓"),
        (CategoryType.Expense, "Gasoline", "⛽"),
        (CategoryType.Both, "Gifts", "🎁"),
        (CategoryType.Expense, "Groceries", "🛒"),
        (CategoryType.Expense, "Health", "🏥"),
        (CategoryType.Expense, "House", "🏠"),
        (CategoryType.Expense, "Mobile Communication", "📱"),
        (CategoryType.Expense, "Personal Expenses", "💪"),
        (CategoryType.Income, "Salary", "💵"),
        (CategoryType.Expense, "Taxi", "🚕"),
        (CategoryType.Expense, "Trips", "✈️"),
        (CategoryType.Expense, "Utilities", "💡"),
    };

    public CategoriesInitializer(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }

    public async Task InitializeDefaults(Guid userId, CancellationToken cancellationToken)
    {
        if (await _categoryManager.Exists(userId, cancellationToken))
            return;

        var createCommands = _defaultCategories
            .Select(c => new CreateCategoryDto
            {
                UsertId = userId,
                CategoryType = c.CategoryType,
                Title = c.Title,
                Emoji = c.Emoji,
                ParentCategoryId = null
            });

        foreach (var category in createCommands)
            await _categoryManager.Create(category, cancellationToken);
    }
}