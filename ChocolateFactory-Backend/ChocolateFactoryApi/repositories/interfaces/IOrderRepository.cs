using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;

namespace ChocolateFactoryApi.repositories.interfaces
{
    public interface IOrderRepository
    {
        Task createOrderAsync(Order order);

        Task<List<OrderResponseDto>> GetOrdersAsync();
        Task<Order> getOrderById(int id);
        Task deleteOrderAsync(Order order);
        Task updateOrderAsync(Order order);
    }
}
