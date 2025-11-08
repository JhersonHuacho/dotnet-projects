using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Customers.Create
{
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
	{
		private readonly ILogger<CreateCustomerCommandHandler> _logger;
		private readonly ICustomerRepository _customerRepository;
		private readonly IMusicStoreUnitOfWork _musicStoreUnitOfWork;

		public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, ICustomerRepository customerRepository, IMusicStoreUnitOfWork musicStoreUnitOfWork)
		{
			_logger = logger;
			_customerRepository = customerRepository;
			_musicStoreUnitOfWork = musicStoreUnitOfWork;
		}

		public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			var email = Email.Create(request.Email);
			var customer = Customer.Create(
				email,
				request.FirstName,
				request.LastName);
			if (customer == null)
			{
				throw new ApplicationException("Error creating customer");
			}

			var result = await _customerRepository.AddAsync(customer);

			await _musicStoreUnitOfWork.SaveChangesAsync();

			return result.Id;
		}
	}
}
