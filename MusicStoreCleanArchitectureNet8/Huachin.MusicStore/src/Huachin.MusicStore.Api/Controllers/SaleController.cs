using Huachin.MusicStore.Application.Handlers.Sales.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Huachin.MusicStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		private readonly ILogger<SaleController> _logger;
		private readonly IMediator _mediator;
		public SaleController(ILogger<SaleController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction("GetAllSales", new { id = result });
		}
	}
}
