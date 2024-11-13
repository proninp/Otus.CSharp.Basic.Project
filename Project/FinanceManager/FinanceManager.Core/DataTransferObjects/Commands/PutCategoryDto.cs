using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands;
public class PutCategoryDto : BasePutDto<Category>
{
    public Guid? Id { get; init; }

    public Guid UsertId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public override Category ToModel() =>
        new Category(UsertId, Title, ParentCategoryId);
}
