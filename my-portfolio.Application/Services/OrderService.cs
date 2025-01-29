using Application.Dtos;
using Application.Interfaces.Services;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Models.Entities;

namespace Application.Services;

public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        // Método para criar um pedido a partir de um OrderRequestDTO
        public async Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO orderRequest, CancellationToken cancellationToken)
        {
            // Criação do pedido
            var order = new Order
            {
                TotalOrder = orderRequest.TotalOrder,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ShippingAddress = orderRequest.ShippingAddress
            };

            // Adiciona os produtos à ordem, incluindo a quantidade
            foreach (var productRequest in orderRequest.Products)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productRequest.ProductId);
                if (product != null)
                {
                    // Adiciona o produto com a quantidade à ordem
                    order.AddProduct(product, productRequest.Quantity);
                }
            }

            // Salvar no banco de dados
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync(cancellationToken);

            // Retornar resposta com dados da ordem
            return new OrderResponseDTO
            {
                OrderId = order.Id,
                TotalOrder = order.TotalOrder,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Products = order.OrderProducts.Select(op => new ProductResponseDTO
                {
                    ProductId = op.Product.Id,
                    Name = op.Product.Name,
                    Quantity = op.Quantity, // A quantidade de cada produto no pedido
                    Price = op.Product.GetDiscountedPrice() // Preço com desconto
                }).ToList()
            };
        }
    }