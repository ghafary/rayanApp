using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Extention;
using ProductApp.Application.Products.Commands.CreateProduct;
using System.Net;
using ProductApp.Application.Products.Commands.UpdateProduct;
using ProductApp.Application.Products.Commands.DeleteProduct;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using ProductApp.Application.Products.Queries.GetProductsWithPagination;

namespace Products.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IMediator mediator,
            IIdentityService identityService,
            ILogger<ProductController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProductAsync(
             [FromBody] CreateProductCommand createProductCommand)
        {
            _logger.LogInformation(
                "Sending command: {CommandName} - {NameProperty}: {CommandId} ({@Command})",
                createProductCommand.GetGenericTypeName(),
                nameof(createProductCommand.Name),
                createProductCommand.Name,
                createProductCommand);

            return await _mediator.Send(createProductCommand);
        }

        [HttpPut("{productId:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Update(int productId, UpdateProductCommand command)
        {
            if (productId != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [Route("{productId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(ProductBriefDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductBriefDto>> GetProductAsync(int productId)
        {
            try
            {
                var product = await _mediator.Send(new GetProductWithProductIdQuery { ProductId = productId });
                return Ok(product);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductSummary>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductSummary>>> GetProductsAsync([FromQuery] GetProductsWithPaginationQuery query)
        {
            var products = await _mediator.Send(query);
            return Ok(products);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}