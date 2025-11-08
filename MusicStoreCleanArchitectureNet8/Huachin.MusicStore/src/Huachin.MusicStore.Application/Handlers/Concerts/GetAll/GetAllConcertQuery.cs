using Huachin.MusicStore.Application.Dtos.Concerts;
using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Concerts.GetAll
{
	public record GetAllConcertQuery() : IRequest<List<ListaEventosResponseDto>>;
}
