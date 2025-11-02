using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Dto.Request.Generos;
using Huachin.MusicStore.Dto.Response;
using Huachin.MusicStore.Dto.Response.Generos;
using Huachin.MusicStore.Repositorios.Interfaces;
using Huachin.MusicStore.Servicio.Interfaces;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Servicio.Implementaciones
{
	public class GenreServicio : IGenreServicio
	{
		private readonly IGenreRepositorio _genreRepositorio;
		private readonly ILogger<GenreServicio> _logger;

		public GenreServicio(IGenreRepositorio genreRepositorio, ILogger<GenreServicio> logger)
		{
			_genreRepositorio = genreRepositorio;
			_logger = logger;
		}

		public async Task<BaseResponse> Registrar(GenreRequestDto request)
		{
			var response = new BaseResponse();
			try
			{
				var genre = new Genre()
				{
					Name = request.Name,
					Estado = request.Estado
				};

				await _genreRepositorio.AddAsync(genre);

				response.Message = "Género registrado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al registrar el género";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");				
			}

			return response;
		}

		public async Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request)
		{
			var response = new BaseResponse();

			try
			{
				var genre = await _genreRepositorio.FindAsync(id);
				if (genre is null)
				{
					response.Message = "Género no encontrado";
					response.Success = false;
					return response;
				}
				genre.Name = request.Name;
				await _genreRepositorio.UpdateAsync();
				response.Message = "Género actualizado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al actualizar el género";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

		public async Task<BaseResponse> DeleteAsync(int id)
		{
			var response = new BaseResponse();
			try
			{
				var genre = await _genreRepositorio.FindAsync(id);
				if (genre is null)
				{
					response.Message = "Género no encontrado";
					response.Success = false;
					return response;
				}

				await _genreRepositorio.DeleteAsync(id);
				response.Message = "Género eliminado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al eliminar el género";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

		public async Task<BaseResponseGeneric<IEnumerable<GenreResponseDto>>> GetAsyn()
		{
			var response = new BaseResponseGeneric<IEnumerable<GenreResponseDto>>();
			try
			{
				var genres = await _genreRepositorio.ListAsync(predicado: x => x.Estado == true);
				response.Data = genres.Select(x => new GenreResponseDto
				{
					Id = x.Id,
					Name = x.Name
				});
				response.Message = "Géneros obtenidos correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al obtener los géneros";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}
			return response;
		}

		public async Task<BaseResponseGeneric<GenreResponseDto>> GetAsyn(int id)
		{
			var response = new BaseResponseGeneric<GenreResponseDto>();
			try
			{
				var genre = await _genreRepositorio.FindAsync(id);
				if (genre is null)
				{
					response.Message = "Género no encontrado";
					response.Success = false;
					return response;
				}

				response.Data = new GenreResponseDto
				{
					Id = genre.Id,
					Name = genre.Name
				};
				response.Message = "Géneros obtenidos correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al obtener el genero";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

		public async Task<BaseResponseGeneric<IEnumerable<ListaClientesResponseDto>>> Listar(BusquedaGenerosRequest request)
		{
			var response = new BaseResponseGeneric<IEnumerable<ListaClientesResponseDto>>();
			try
			{
				var genres = await _genreRepositorio.ListAsync(
					predicado: x => x.Estado == true && (string.IsNullOrWhiteSpace(request.Name) || x.Name.Contains(request.Name)));

				response.Data = genres.Select(x => new ListaClientesResponseDto
				{
					Id = x.Id,
					Name = x.Name,
					Estado = x.Estado
				});
				response.Message = "Géneros obtenidos correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al obtener los géneros";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}
			return response;
		}
	}
}
