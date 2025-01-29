namespace Domain.Models.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    // Método para verificar se o produto está em estoque
    public bool IsInStock(int quantity) => StockQuantity >= quantity;

    // Método para calcular o preço com desconto
    public decimal GetDiscountedPrice() => Price * 0.9m; // Exemplo de desconto de 10%
}