using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.ViewModels;
using OrderManagement.Application;
using OrderManagement.Api.Extensions;
using OrderManagement.Application.Commands.Orders;
using OrderManagement.Application.Queries.Orders;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [EndpointSummary("Get all orders.")]
        [EndpointDescription("This endpoint is responsible for get all orders.")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());

            return result.ToActionResult(this);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [EndpointSummary("Get a order by id.")]
        [EndpointDescription("This endpoint is responsible for get a order by id.")]
        public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdQuery request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Create new order.")]
        [EndpointDescription("This endpoint is responsible for create a new order.")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPut("UpdateStatus")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Update a order status.")]
        [EndpointDescription("This endpoint is responsible for update a order status.")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Delete a order.")]
        [EndpointDescription("This endpoint is responsible for delete a order.")]
        public async Task<IActionResult> DeleteOrders([FromBody] DeleteOrderCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }
    }
}
