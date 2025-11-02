using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Dto.Request.Eventos;
using Huachin.MusicStore.Dto.Request.Generos;
using Huachin.MusicStore.Dto.Response;
using Huachin.MusicStore.Dto.Response.Eventos;
using Huachin.MusicStore.Repositorios.Interfaces;
using Huachin.MusicStore.Servicio.Interfaces;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Servicio.Implementaciones
{
    public class ConcertServicio : IConcertServicio
	{
        private readonly IConcertRepositorio _concertRepositorio;
		private readonly IGenreRepositorio _genreRepositorio;
		private readonly ILogger<ConcertServicio> _logger;

		public ConcertServicio(IConcertRepositorio concertRepositorio, ILogger<ConcertServicio> logger, IGenreRepositorio genreRepositorio)
		{
			_concertRepositorio = concertRepositorio;
			_logger = logger;
			_genreRepositorio = genreRepositorio;
		}

		public async Task<BaseResponse> Registrar(ConcertRequestDto request)
		{
			var response = new BaseResponse();
			try
			{
				var genre = new Concert()
				{
					IdGenre = request.IdGenre,
					Title = request.Title,
					Description = request.Description,
					Place = request.Place,
					UnitPrice = request.UnitPrice,
					DateEvent = Convert.ToDateTime(request.Fecha),
					ImageUrl = request.ImageUrl ?? "",
					TicketsQuantity = request.TicketsQuantity,
					Finalized = false
				};

				await _concertRepositorio.AddAsync(genre);

				response.Message = "Concierto registrado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al registrar un concierto";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

		public async Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request)
		{
			var response = new BaseResponse();

			try
			{
				var concert = await _concertRepositorio.FindAsync(id);
				if (concert is null)
				{
					response.Message = "Evento no encontrado";
					response.Success = false;
					return response;
				}

				concert.IdGenre = request.IdGenre;
				concert.Title = request.Title;
				concert.Description = request.Description;
				concert.Place = request.Place;
				concert.UnitPrice = request.UnitPrice;
				concert.DateEvent = Convert.ToDateTime(request.Fecha);
				concert.ImageUrl = request.ImageUrl ?? "";
				concert.TicketsQuantity = request.TicketsQuantity;
				concert.Finalized = false;

				await _concertRepositorio.UpdateAsync();
				response.Message = "Evento actualizado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al actualizar el evento";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

		public async Task<BaseResponseGeneric<IEnumerable<ListaEventosResponseDto>>> Listar(BusquedaEventosRequest request)
		{
			var response = new BaseResponseGeneric<IEnumerable<ListaEventosResponseDto>>();
			try
			{
				var conciertos = await _concertRepositorio.ListAsync(
					predicado: x => x.Estado == true && (string.IsNullOrWhiteSpace(request.Title) || x.Title.Contains(request.Title)));

				foreach (var concierto in conciertos)
				{
					var genre = await _genreRepositorio.FindAsync(concierto.IdGenre);
					concierto.IdGenreNavigation = genre;
				}				

				response.Data = conciertos.Select(x => new ListaEventosResponseDto
				{
					IdEvento = x.Id,
					NameGenre = x.IdGenreNavigation.Name,
					Title = x.Title,
					Description = x.Description,
					Place = x.Place,
					UnitPrice = x.UnitPrice,
					DateEvent = x.DateEvent,
					ImageUrl = x.ImageUrl ?? "concierto-default.jpeg",// "https://picsum.photos/200/300", //"https://loremflickr.com/100/100?random=2",
					TicketsQuantity = x.TicketsQuantity,
					Estado = x.Estado
				});
				response.Message = "Eventos obtenidos correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al obtener los eventos";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}
			return response;
		}

		public async Task<BaseResponseGeneric<ListaEventosResponseDto>> GetEventoById(int id)
		{
			var response = new BaseResponseGeneric<ListaEventosResponseDto>();
			try
			{
				var concierto = await _concertRepositorio.FindAsync(id);

				var genre = await _genreRepositorio.FindAsync(concierto.IdGenre);
				concierto.IdGenreNavigation = genre;

				response.Data = new ListaEventosResponseDto
				{
					IdEvento = concierto.Id,
					NameGenre = concierto.IdGenreNavigation.Name,
					Title = concierto.Title,
					Description = concierto.Description,
					Place = concierto.Place,
					UnitPrice = concierto.UnitPrice,
					DateEvent = concierto.DateEvent,
					ImageUrl = concierto.ImageUrl ?? "concierto-default.jpeg",// "https://picsum.photos/200/300", //"https://loremflickr.com/100/100?random=2",
					TicketsQuantity = concierto.TicketsQuantity,
					Estado = concierto.Estado
				};
				response.Message = "Evento obtenido correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al obtener el evento";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}
			return response;
		}
	}
}
