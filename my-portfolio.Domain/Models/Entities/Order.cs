using Domain.Enums;

namespace Domain.Models.Entities;

// Representa a ordem de compra
public class Order
{
    public Guid Id { get; set; }
    public decimal TotalOrder { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>(); // Relacionamento com produtos e quantidade

    // Método para calcular o total da ordem com base nos produtos
    private void CalculateTotal() =>
        TotalOrder = OrderProducts.Sum(op => op.Product.GetDiscountedPrice() * op.Quantity); // Soma o preço com desconto dos produtos, multiplicado pela quantidade

    // Método para adicionar um produto à ordem, caso esteja em estoque
    public void AddProduct(Product product, int quantity)
    {
        if (product.IsInStock(quantity))
        {
            var orderProduct = new OrderProduct
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            };

            OrderProducts.Add(orderProduct);
            CalculateTotal(); // Recalcula o total após a adição
        }
        else
        {
            throw new InvalidOperationException("Produto fora de estoque.");
        }
    }

    // Método para atualizar o status da ordem
    public void UpdateStatus(OrderStatus newStatus) => Status = newStatus;
}