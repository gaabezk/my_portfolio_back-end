namespace Domain.Models.Entities;

// Representa a junção entre pedido e produto
public class OrderProduct
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } // Relacionamento com a ordem
    public Guid ProductId { get; set; }
    public Product Product { get; set; } // Relacionamento com o produto
    public int Quantity { get; set; } // Quantidade do produto no pedido
}