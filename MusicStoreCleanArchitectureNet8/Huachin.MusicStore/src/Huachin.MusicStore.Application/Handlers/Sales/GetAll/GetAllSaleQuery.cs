using Huachin.MusicStore.Application.Dtos.Sales;
using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Sales.GetAll
{
	public record GetAllSaleQuery() : IRequest<List<ListaVentasResponseDto>>;
}
