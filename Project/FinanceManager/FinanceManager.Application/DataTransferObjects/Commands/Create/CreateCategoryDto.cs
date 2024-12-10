using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateCategoryDto
{
    public Guid UsertId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public Category ToModel() =>
        new Category(UsertId, Title, ParentCategoryId);
}
