using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Concerts.Create
{
	public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, Guid>
	{
		private readonly IConcertRepository _concertRepository;
		private readonly IMusicStoreUnitOfWork _musicStoreUnitOfWork;
		private readonly ILogger<CreateConcertCommandHandler> _logger;

		public CreateConcertCommandHandler(
			IConcertRepository concertRepository, 
			IMusicStoreUnitOfWork musicStoreUnitOfWork, 
			ILogger<CreateConcertCommandHandler> logger)
		{
			_concertRepository = concertRepository;
			_musicStoreUnitOfWork = musicStoreUnitOfWork;
			_logger = logger;
		}

		public async Task<Guid> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
		{
			var unitPrice = Money.Create(request.UnitPrice, "USD");
			var dateEvent = DateTime.ParseExact(
				$"{request.Fecha} {request.Hora}", 
				"yyyy-MM-dd HH:mm", 
				System.Globalization.CultureInfo.InvariantCulture);
			var concert = Concert.Create(
				request.IdGenre,
				request.Title,
				request.Description,
				request.Place,
				unitPrice,
				dateEvent,
				request.ImageUrl,
				request.TicketsQuantity,
				request.Estado);

			var result = await _concertRepository.AddAsync(concert);

			if (result == null)
			{
				_logger.LogError("Error creating concert: {@Concert}", concert);
				throw new ApplicationException("Error creating concert");
			}

			await _musicStoreUnitOfWork.SaveChangesAsync();

			return result.Id;
		}
	}
}
