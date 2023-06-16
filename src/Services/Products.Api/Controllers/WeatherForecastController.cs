using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using MediatR;
using Product.Application.Common.Interfaces;

namespace Products.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly ILogger<DemoController> _logger;

        public DemoController(
            IMediator mediator,
            IIdentityService identityService,
            ILogger<DemoController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}