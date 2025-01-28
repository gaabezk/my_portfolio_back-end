namespace Domain.Models.Entities;

public class Order
{
    public Guid Id { get; set; }
    public decimal TotalOrder { get; set; }
    public IEnumerable<Product> Products { get; set; }
}