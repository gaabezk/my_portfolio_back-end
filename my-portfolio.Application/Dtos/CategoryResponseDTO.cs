namespace Application.Dtos;

public class CategoryResponseDTO
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string FullCategoryName { get; set; } // Nome completo incluindo categoria pai
    public DateTime CreatedAt { get; set; }
}