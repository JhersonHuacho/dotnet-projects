using Huachin.MusicStore.Application.Dtos;
using Huachin.MusicStore.Application.Dtos.Genres;
using Huachin.MusicStore.Application.Handlers.Genres.Create;
using Huachin.MusicStore.Application.Handlers.Genres.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Huachin.MusicStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly ILogger<GenreController> _logger;
		private readonly IMediator _mediator;

		public GenreController(ILogger<GenreController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllGenres()
		{
			var query = new GetAllGenreQuery();
			var result = await _mediator.Send(query);
			
			return Ok(BaseResponse<ICollection<GenreResponseDto>>.Success(result));
		}

		[HttpPost]
		public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction(nameof(GetAllGenres), new { id = result });
		}
	}
}
