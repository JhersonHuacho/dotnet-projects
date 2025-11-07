using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Sales.Create
{
	public record CreateSaleCommand(
		int IdCustomer,
		int IdConcert,
		DateTime SaleDate,
		string OperationNumber,
		decimal Total,
		short Quantity) : IRequest<Guid>;	
}
