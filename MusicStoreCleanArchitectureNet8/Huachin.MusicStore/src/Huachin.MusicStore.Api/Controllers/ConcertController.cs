using Huachin.MusicStore.Application.Handlers.Concerts.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Huachin.MusicStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConcertController : ControllerBase
	{
		private readonly ILogger<ConcertController> _logger;
		private readonly IMediator _mediator;

		public ConcertController(ILogger<ConcertController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateConcert([FromBody] CreateConcertCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction("GetAllConcerts", new { id = result });
		}

	}
}
