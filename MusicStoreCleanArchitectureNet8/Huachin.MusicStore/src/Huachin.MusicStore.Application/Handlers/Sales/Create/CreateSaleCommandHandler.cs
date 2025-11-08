using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Sales.Create
{
	public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
	{
		private readonly ISaleRepository _saleRepository;
		private readonly IMusicStoreUnitOfWork _musicStoreUnitOfWork;
		private readonly ILogger<CreateSaleCommandHandler> _logger;

		public CreateSaleCommandHandler(ISaleRepository saleRepository, IMusicStoreUnitOfWork musicStoreUnitOfWork, ILogger<CreateSaleCommandHandler> logger)
		{
			_saleRepository = saleRepository;
			_musicStoreUnitOfWork = musicStoreUnitOfWork;
			_logger = logger;
		}

		public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
		{
			var total = Money.Create(request.Total, "USD");
			var dateEvent = DateTime.SpecifyKind(request.SaleDate, DateTimeKind.Utc);
			var sale = Sale.Create(
				request.IdCustomer,
				request.IdConcert,
				dateEvent,
				request.OperationNumber,
				total,
				request.Quantity);

			if (sale == null)
			{
				_logger.LogError("Error creating sale: {@Sale}", sale);
				throw new ApplicationException("Error creating sale");
			}

			var result = await _saleRepository.AddAsync(sale);

			await _musicStoreUnitOfWork.SaveChangesAsync();

			return result.Id;
		}
	}
}
