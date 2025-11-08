using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Customers.Create
{
	public record CreateCustomerCommand(string FirstName, string LastName, string Email) : IRequest<Guid>;
}
