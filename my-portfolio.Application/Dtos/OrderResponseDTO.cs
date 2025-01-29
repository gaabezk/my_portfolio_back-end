using Domain.Enums;

namespace Application.Dtos;

public class OrderResponseDTO
{
    public Guid OrderId { get; set; }
    public decimal TotalOrder { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ProductResponseDTO> Products { get; set; } = new List<ProductResponseDTO>(); // Lista de produtos no pedido
}