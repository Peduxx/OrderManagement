using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Extensions;
using OrderManagement.Application;
using OrderManagement.Application.Commands.Custormers;
using OrderManagement.Application.Queries.Customers;
using OrderManagement.Application.ViewModels;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerViewModel>), 200)]
        [EndpointSummary("Get all customers.")]
        [EndpointDescription("This endpoint is responsible for get all customers.")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());

            return result.ToActionResult(this);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        [EndpointSummary("Get a customer by id.")]
        [EndpointDescription("This endpoint is responsible for get a customer by id.")]
        public async Task<IActionResult> GetCustomerById([FromQuery] GetCustomerByIdQuery request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Create new customer.")]
        [EndpointDescription("This endpoint is responsible for create a new customer.")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Update a customer.")]
        [EndpointDescription("This endpoint is responsible for update a customer.")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Delete a customer.")]
        [EndpointDescription("This endpoint is responsible for delete a customer.")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }
    }
}
