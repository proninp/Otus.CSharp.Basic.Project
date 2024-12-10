using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class CategoryDto : IdentityDtoBase
{
    public Guid UserId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }
}

public static class CategoryMappings
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            UserId = category.UserId,
            Title = category.Title,
            ParentCategoryId = category.ParentCategoryId,
        };
    }
}