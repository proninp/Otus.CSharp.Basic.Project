using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Enums;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateCategoryDto : IdentityDtoBase
{
    public Guid UsertId { get; init; }

    public CategoryType CategoryType { get; set; }

    public string? Title { get; set; }

    public string? Emoji { get; set; }

    public Guid? ParentCategoryId { get; set; }
}