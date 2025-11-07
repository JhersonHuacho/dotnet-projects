using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Genres.Create
{
	public record CreateGenreCommand(string Name, bool Estado) : IRequest<Guid>;	
}
