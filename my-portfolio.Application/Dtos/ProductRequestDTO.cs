namespace Application.Dtos;

public class ProductRequestDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } // Quantidade do produto no pedido
}