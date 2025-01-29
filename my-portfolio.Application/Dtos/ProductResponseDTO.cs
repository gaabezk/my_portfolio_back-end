namespace Application.Dtos;

public class ProductResponseDTO
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; } // Quantidade de cada produto
    public decimal Price { get; set; } // Preço com desconto
}