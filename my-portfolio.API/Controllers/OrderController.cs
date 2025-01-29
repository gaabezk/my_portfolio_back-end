using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderRequestDTO orderRequest)
    {
        var orderResponse = await _orderService.CreateOrderAsync(orderRequest);
        return Ok(orderResponse);
    }
}