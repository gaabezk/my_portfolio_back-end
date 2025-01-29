namespace Application.Dtos;

public class CategoryRequestDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}