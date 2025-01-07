using FinanceManager.Core.Enums;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateCategoryDto
{
    public Guid UsertId { get; init; }

    public CategoryType CategoryType { get; init; }

    public string? Title { get; set; }

    public string? Emoji { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public Category ToModel() =>
        new Category(UsertId, CategoryType, Title, Emoji, ParentCategoryId);
}
