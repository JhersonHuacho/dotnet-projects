using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Concerts.Create
{
	public record CreateConcertCommand(
		int IdGenre,
		string Title,
		string Description,
		string Place,
		decimal UnitPrice,
		string Fecha,
		string Hora,
		string? ImageUrl,
		int TicketsQuantity,
		bool Estado) : IRequest<Guid>;
}
