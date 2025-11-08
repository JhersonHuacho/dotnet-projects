using Huachin.MusicStore.Application.Dtos;
using Huachin.MusicStore.Application.Dtos.Concerts;
using Huachin.MusicStore.Application.Dtos.Genres;
using Huachin.MusicStore.Application.Handlers.Concerts.Create;
using Huachin.MusicStore.Application.Handlers.Concerts.GetAll;
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

		[HttpGet]
		public async Task<IActionResult> GetAllConcerts()
		{
			var result = await _mediator.Send(new GetAllConcertQuery());
			return Ok(BaseResponse<ICollection<ListaEventosResponseDto>>.Success(result));
		}

		[HttpPost]
		public async Task<IActionResult> CreateConcert([FromBody] CreateConcertCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction("GetAllConcerts", new { id = result });
		}

	}
}
