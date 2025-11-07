using Huachin.MusicStore.Application.Dtos.Genres;
using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Genres.GetAll
{
	public record GetAllGenreQuery() : IRequest<ICollection<GenreResponseDto>>;	
}
