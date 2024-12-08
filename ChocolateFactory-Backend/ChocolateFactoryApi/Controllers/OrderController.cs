using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }

        [HttpGet]
        public async Task<IActionResult> getOrders()
        {
            return Ok(await _orderRepository.GetOrdersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> createOrder(OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto.OrderDate <= DateTime.Now.ToUniversalTime())
            {
                return BadRequest("order date cannot be in the past");
            }
            Order order = new Order()
            {
                CustomerId = orderRequestDto.CustomerId,
                ProductId= orderRequestDto.ProductId,
                Quantity = orderRequestDto.Quantity,
                OrderDate = orderRequestDto.OrderDate,
                DeliveryDate = orderRequestDto.DeliveryDate,
                status = orderRequestDto.status,
            };

            await _orderRepository.createOrderAsync(order);
            return StatusCode(StatusCodes.Status201Created, "Order created");
        }

        [HttpPut]
        public async Task<IActionResult> updateOrder(int id,OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto.OrderDate <= DateTime.Now.ToUniversalTime())
            {
                return BadRequest("order date cannot be in the past");
            }

            Order order = await _orderRepository.getOrderById(id);

            order.CustomerId = orderRequestDto.CustomerId;
            order.ProductId = orderRequestDto.ProductId;
            order.Quantity = orderRequestDto.Quantity;
            order.OrderDate = orderRequestDto.OrderDate;
            order.DeliveryDate = orderRequestDto.DeliveryDate;
            order.status = orderRequestDto.status;
            

            await _orderRepository.updateOrderAsync(order);
            return Ok("Order updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> deleteOrder(int id)
        {
            Order order = await _orderRepository.getOrderById(id);
            await _orderRepository.deleteOrderAsync(order);
            return Ok("order deleted successfully");
        }
    }
}
