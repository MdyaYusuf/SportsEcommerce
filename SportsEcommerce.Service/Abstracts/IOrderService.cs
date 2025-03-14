using Core.Responses;
using SportsEcommerce.Models.Dtos.Orders.Requests;
using SportsEcommerce.Models.Dtos.Orders.Responses;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Abstracts;

public interface IOrderService
{
  Task<ReturnModel<OrderResponseDto>> CreateOrderAsync(CreateOrderRequest request, Cart cart);
  Task<ReturnModel<OrderResponseDto>> GetOrderByIdAsync(int orderId);
}
