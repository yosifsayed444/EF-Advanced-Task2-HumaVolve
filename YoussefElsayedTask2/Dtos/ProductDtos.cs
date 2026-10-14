using System.ComponentModel.DataAnnotations;

namespace YoussefElsayedTask2.Dtos;

public record ProductDto(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int CategoryId,
    string CategoryName,
    byte[] RowVersion);

public record PagedResult<T>(int Page, int PageSize, int TotalCount, List<T> Items);

public class UpdateProductDto
{
    [Required, MaxLength(200)] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Range(0, double.MaxValue)] public decimal Price { get; set; }
    [Required] public byte[] RowVersion { get; set; } = [];
}
