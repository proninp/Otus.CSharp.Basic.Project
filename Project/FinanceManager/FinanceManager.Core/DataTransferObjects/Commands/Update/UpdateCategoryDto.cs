using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Update;
public sealed class UpdateCategoryDto : IdentityDtoBase, IPutModel<Category>
{
    public Guid UsertId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public Category ToModel() =>
        new Category(UsertId, Title, ParentCategoryId);
}
