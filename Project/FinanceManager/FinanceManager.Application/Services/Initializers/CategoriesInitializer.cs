using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;

namespace FinanceManager.Application.Services.Initializers;
public class CategoriesInitializer : ICategoriesInitializer
{
    private readonly ICategoryManager _categoryManager;
    private readonly (string Title, string Emoji)[] _defaultCategories = new[]
    {
        ("Car", "🚗"),
        ("Cafes and Restaurants", "🍽️"),
        ("Clothing", "🧥"),
        ("Deposit Interest", "💰"),
        ("Education", "🎓"),
        ("Gasoline", "⛽"),
        ("Gifts", "🎁"),
        ("Groceries", "🛒"),
        ("Health", "🏥"),
        ("House", "🏠"),
        ("Mobile Communication", "📱"),
        ("Personal Expenses", "💪"),
        ("Salary", "💵"),
        ("Taxi", "🚕"),
        ("Trips", "✈️"),
        ("Utilities", "💡"),
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
                Title = c.Title,
                Emoji = c.Emoji,
                ParentCategoryId = null
            });

        foreach (var category in createCommands)
            await _categoryManager.Create(category, cancellationToken);
    }
}