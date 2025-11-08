using Huachin.MusicStore.Application.Dtos;
using Huachin.MusicStore.Application.Dtos.Sales;
using Huachin.MusicStore.Application.Handlers.Sales.Create;
using Huachin.MusicStore.Application.Handlers.Sales.GetAll;
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

		[HttpGet]
		public async Task<IActionResult> GetAllSales()
		{
			var result = await _mediator.Send(new GetAllSaleQuery());
			return Ok(BaseResponse<ICollection<ListaVentasResponseDto>>.Success(result));
		}

		[HttpPost]
		public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction("GetAllSales", new { id = result });
		}
	}
}
