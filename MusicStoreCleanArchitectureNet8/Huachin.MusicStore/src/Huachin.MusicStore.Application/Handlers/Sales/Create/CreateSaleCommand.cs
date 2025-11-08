using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Sales.Create
{
	public record CreateSaleCommand(
		Guid IdCustomer,
		Guid IdConcert,
		DateTime SaleDate,
		string OperationNumber,
		decimal Total,
		short Quantity) : IRequest<Guid>;	
}
