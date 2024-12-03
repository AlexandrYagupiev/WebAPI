using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Order.Interfaces;
using WebAPI.Order.Model;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            await _publishEndpoint.Publish<OrderCreated>(new
            {
                Id = Guid.NewGuid(),
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            });

            return Ok();
        }

    }
}
