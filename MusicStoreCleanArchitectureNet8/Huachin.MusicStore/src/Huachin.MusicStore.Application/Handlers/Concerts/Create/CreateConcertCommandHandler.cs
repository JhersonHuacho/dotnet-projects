using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

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
			try
			{
				var unitPrice = Money.Create(request.UnitPrice, "USD");
				var dateEvent = ParseDateTimeAsUtcV2(request.Fecha, request.Hora);
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
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error");
				throw;
			}			
		}

		/// <summary>
		/// Convierte fecha y hora separadas a DateTime UTC, pero considerando la zona horaria local del servidor.
		/// IMPORTANTE: Este método asume que el input representa HORA LOCAL del servidor.
		/// Ejemplo: Si el servidor está en Lima (UTC-5) y recibe "20:00", 
		/// lo convertirá a "01:00 UTC del día siguiente"
		/// </summary>
		/// <param name="fecha">Fecha en formato yyyy-MM-dd (hora local)</param>
		/// <param name="hora">Hora en formato HH:mm:ss (hora local)</param>
		/// <returns>DateTime con Kind = UTC</returns>
		private static DateTime ParseDateTimeAsUtc(string fecha, string hora)
		{
			var dateTimeString = $"{fecha} {hora}";

			// Parsear como DateTime con Kind = Unspecified
			var localDateTime = DateTime.ParseExact(
				dateTimeString,
				"yyyy-MM-dd HH:mm:ss",
				CultureInfo.InvariantCulture);

			// ✅ Convertir hora local del servidor a UTC
			// Esto toma en cuenta automáticamente la zona horaria del servidor
			return localDateTime.ToUniversalTime();  // ← Convierte pero mantiene offset local
		}

		/// <summary>
		/// Convierte fecha y hora separadas a DateTime UTC puro (sin offset)
		/// </summary>
		/// <param name="fecha">Fecha en formato yyyy-MM-dd (hora local del servidor)</param>
		/// <param name="hora">Hora en formato HH:mm:ss (hora local del servidor)</param>
		/// <returns>DateTime con Kind = UTC (sin información de zona horaria)</returns>
		private static DateTime ParseDateTimeAsUtcV2(string fecha, string hora)
		{
			var dateTimeString = $"{fecha} {hora}";

			// ✅ Convertir directamente a UTC puro
			var utcDateTime = DateTime.ParseExact(
				dateTimeString,
				"yyyy-MM-dd HH:mm:ss",
				CultureInfo.InvariantCulture,
				DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);

			return DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
		}
	}
}
