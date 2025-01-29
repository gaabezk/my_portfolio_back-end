namespace Domain.Models.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ParentCategoryId { get; set; } // Categoria pai, pode ser nulo

    public Category? ParentCategory { get; set; } // Navegação para categoria pai, pode ser nulo

    // Método para verificar se a categoria tem uma categoria pai
    public bool HasParentCategory() => ParentCategoryId.HasValue;

    // Método para obter o nome completo da categoria (incluindo a categoria pai, se houver)
    public string GetFullCategoryName() =>
        HasParentCategory() && ParentCategory != null
            ? $"{ParentCategory.Name} > {Name}"
            : Name;
}