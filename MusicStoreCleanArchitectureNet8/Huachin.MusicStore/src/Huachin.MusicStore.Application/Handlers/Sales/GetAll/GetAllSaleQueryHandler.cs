using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Dtos.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Sales.GetAll
{
	public class GetAllSaleQueryHandler : IRequestHandler<GetAllSaleQuery, List<ListaVentasResponseDto>>
	{
		private readonly ILogger<GetAllSaleQueryHandler> _logger;
		private readonly ISaleRepository _saleRepository;

		public GetAllSaleQueryHandler(ILogger<GetAllSaleQueryHandler> logger, ISaleRepository saleRepository)
		{
			_logger = logger;
			_saleRepository = saleRepository;
		}

		public async Task<List<ListaVentasResponseDto>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Handling GetAllSaleQuery");
			var sales = await _saleRepository.ListAsync();
			_logger.LogInformation("Retrieved {Count} sales", sales.Count);

			var salesDto = sales.Select(sale => new ListaVentasResponseDto
			{
				IdSale = sale.Id,
				IdConcert = sale.Concert.Id,
				TitleEvento = sale.Concert.Title,
				Quantity = sale.Quantity,
				TotalPrice = sale.Total.Amount,
				SaleDate = sale.SaleDate
			}).ToList();
			return salesDto;
		}
	}
}
