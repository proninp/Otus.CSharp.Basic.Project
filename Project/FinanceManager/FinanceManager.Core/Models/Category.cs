using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;

public class Category : BaseModel
{
    public Guid UserId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }

    public ICollection<Category>? SubCategories { get; }

    public Category(Guid userId, string? title = null, Guid? parentCategoryId = null)
    {
        UserId = userId;
        Title = title;
        ParentCategoryId = parentCategoryId;
    }
}
