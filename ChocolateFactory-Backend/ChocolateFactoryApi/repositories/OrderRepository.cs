using ChocolateFactoryApi.Data;
using ChocolateFactoryApi.DTO.response;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext appDbContext) 
        {
            context = appDbContext;
        }
        public async Task createOrderAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task deleteOrderAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public Task<Order> getOrderById(int id)
        {
            return context.Orders.Where(o => o.OrderId == id).FirstAsync();
        }

        public async Task<List<OrderResponseDto>> GetOrdersAsync()
        {
            return await context.Orders.Select(o => new OrderResponseDto()
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                ProductId = o.ProductId,
                ProductName = o.Product.ProductName,
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                status = o.status,
            }).ToListAsync();
        }

        public async Task updateOrderAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
    }
}
