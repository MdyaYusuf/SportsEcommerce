using AutoMapper;
using Core.Abstractions;
using Core.Responses;
using Microsoft.EntityFrameworkCore.Storage;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.Models.Dtos.Orders.Requests;
using SportsEcommerce.Models.Dtos.Orders.Responses;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;
using System.Text.Json;

namespace SportsEcommerce.Service.Concretes;

public class OrderService(IOrderRepository _orderRepository, IUnitOfWork _unitOfWork, IMapper _mapper, OrderBusinessRules _businessRules, IProductService _productService) : IOrderService
{
  public async Task<ReturnModel<OrderResponseDto>> CreateOrderAsync(CreateOrderRequest request, Cart cart)
  {
    using (IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync())
    {
      try
      {
        var order = new Order()
        {
          UserId = request.UserId,
          OrderDate = DateTime.Now,
          Total = cart.CartItems.Sum(item => item.UnitPrice * item.Quantity),
          OrderDetails = JsonSerializer.Serialize(cart.CartItems)
        };

        _businessRules.EnsureValidOrder(order);

        foreach (var cartItem in cart.CartItems)
        {
          await _productService.ReduceStockAsync(cartItem.ProductId, cartItem.Quantity);
        }

        await _orderRepository.CreateOrderAsync(order);

        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<OrderResponseDto>(order);

        return new ReturnModel<OrderResponseDto>()
        {
          Success = true,
          Message = "Sipariş başarıyla oluşturuldu.",
          Data = response,
          StatusCode = 201
        };
      }
      catch
      {
        await transaction.RollbackAsync();

        throw;
      }
    }
  }

  public async Task<ReturnModel<OrderResponseDto>> GetOrderByIdAsync(int orderId)
  {
    await _businessRules.IsOrderExistAsync(orderId);

    var order = await _orderRepository.GetOrderByIdAsync(orderId);

    var response = _mapper.Map<OrderResponseDto>(order);

    return new ReturnModel<OrderResponseDto>()
    {
      Success = true,
      Message = "Sipariş başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }
}
