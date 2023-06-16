using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using MediatR;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Extention;
using ProductApp.Application.Products.Commands.CreateProduct;

namespace Products.Api.Controllers
{
    [Route("api/v1/products")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IMediator mediator,
            IIdentityService identityService,
            ILogger<ProductController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
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
    }
}