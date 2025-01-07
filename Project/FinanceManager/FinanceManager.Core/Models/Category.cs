using FinanceManager.Core.Enums;
using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public sealed class Category : IdentityModel
{
    public Guid UserId { get; init; }

    public CategoryType CategoryType { get; set; }

    public string? Title { get; set; }

    public string? Emoji { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }

    public ICollection<Category>? SubCategories { get; }

    public Category(
        Guid userId, CategoryType categoryType = CategoryType.Expense, string? title = null, string? emoji = null, Guid? parentCategoryId = null)
    {
        UserId = userId;
        CategoryType = categoryType;
        Title = title;
        Emoji = emoji;
        ParentCategoryId = parentCategoryId;
    }
}