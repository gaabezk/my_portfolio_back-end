using Application.Dtos;
using Domain.Models.Entities;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO orderRequest, CancellationToken cancellationToken = default);
}