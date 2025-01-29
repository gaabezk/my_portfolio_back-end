namespace Application.Dtos;

public class OrderRequestDTO
{
    public decimal TotalOrder { get; set; }
    public string ShippingAddress { get; set; }
    public List<ProductRequestDTO> Products { get; set; } = new List<ProductRequestDTO>(); // Lista de produtos com a quantidade
}