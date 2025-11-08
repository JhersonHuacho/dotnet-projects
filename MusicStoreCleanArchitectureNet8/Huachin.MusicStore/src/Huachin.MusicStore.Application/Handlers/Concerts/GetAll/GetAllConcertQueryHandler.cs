using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Dtos.Concerts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Concerts.GetAll
{
	public class GetAllConcertQueryHandler : IRequestHandler<GetAllConcertQuery, List<ListaEventosResponseDto>>
	{
		private readonly ILogger<GetAllConcertQueryHandler> _logger;
		private readonly IConcertRepository _concertRepository;

		public GetAllConcertQueryHandler(ILogger<GetAllConcertQueryHandler> logger, IConcertRepository concertRepository)
		{
			_logger = logger;
			_concertRepository = concertRepository;
		}

		public async Task<List<ListaEventosResponseDto>> Handle(GetAllConcertQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Handling GetAllConcertQuery");
			var concerts = await _concertRepository.ListWithGenresAsync();
			_logger.LogInformation("Retrieved {Count} concerts", concerts.Count);

			var concertsDto = concerts.Select(concert => new ListaEventosResponseDto
			{
				IdEvento = concert.Id,
				NameGenre = concert.Genre.Name ?? string.Empty,
				Title = concert.Title,
				Description = concert.Description,
				Place = concert.Place,
				UnitPrice = concert.UnitPrice.Amount,
				DateEvent = concert.DateEvent,
				TicketsQuantity = concert.TicketsQuantity,
				ImageUrl = concert.ImageUrl ?? string.Empty,
				Estado = concert.Finalized
			}).ToList();

			return concertsDto;
		}
	}
}
