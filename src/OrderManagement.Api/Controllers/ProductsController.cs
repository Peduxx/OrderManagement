using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application;
using OrderManagement.Api.Extensions;
using OrderManagement.Application.Commands.Products;
using OrderManagement.Application.Queries.Products;
using OrderManagement.Application.ViewModels;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductViewModel), 200)]
        [EndpointSummary("Get all products.")]
        [EndpointDescription("This endpoint is responsible for get all products.")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());

            return result.ToActionResult(this);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ProductViewModel), 200)]
        [EndpointSummary("Get a product by id.")]
        [EndpointDescription("This endpoint is responsible for get a product by id.")]
        public async Task<IActionResult> GetProductById([FromQuery]GetProductByIdQuery request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Create new product.")]
        [EndpointDescription("This endpoint is responsible for create a new product.")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Update a product.")]
        [EndpointDescription("This endpoint is responsible for update a product.")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(Result), 200)]
        [EndpointSummary("Delete a product.")]
        [EndpointDescription("This endpoint is responsible for delete a product.")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand request)
        {
            var result = await _mediator.Send(request);

            return result.ToActionResult(this);
        }
    }
}
