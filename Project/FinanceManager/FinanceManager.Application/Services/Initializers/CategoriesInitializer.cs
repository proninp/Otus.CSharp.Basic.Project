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
        (CategoryType.Income, "Bank Interest", "🏦"),
        (CategoryType.Expense, "Car", "🚗"),
        (CategoryType.Expense, "Cafes and Restaurants", "🍽️"),
        (CategoryType.Expense, "Clothing", "🧥"),
        (CategoryType.Expense, "Education", "🎓"),
        (CategoryType.Expense, "Entertainment", "🎉"),
        (CategoryType.Expense, "Gasoline", "⛽"),
        (CategoryType.Both, "Gifts", "🎁"),
        (CategoryType.Expense, "Groceries", "🛒"),
        (CategoryType.Expense, "Healthcare", "🏥"),
        (CategoryType.Expense, "House", "🏠"),
        (CategoryType.Income, "Investments", "📈"),
        (CategoryType.Expense, "Mobile Communication", "📱"),
        (CategoryType.Expense, "Personal Expenses", "💪"),
        (CategoryType.Income, "Refund", "💵"),
        (CategoryType.Expense, "Rent", "🏠"),
        (CategoryType.Income, "Rental Income", "🏠"),
        (CategoryType.Income, "Salary", "💼"),
        (CategoryType.Income, "Sales", "🛍"),
        (CategoryType.Expense, "Shopping", "🛍"),
        (CategoryType.Expense, "Taxi", "🚕"),
        (CategoryType.Expense, "Travel", "✈️"),
        (CategoryType.Expense, "Utilities", "💡"),
    };

    public CategoriesInitializer(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }

    public async Task InitializeDefaultsAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (await _categoryManager.ExistsAsync(userId, cancellationToken))
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
            await _categoryManager.CreateAsync(category, cancellationToken);
    }
}