using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Dto.Request.Generos;
using Huachin.MusicStore.Dto.Request.Sale;
using Huachin.MusicStore.Dto.Response;
using Huachin.MusicStore.Repositorios.Implementaciones;
using Huachin.MusicStore.Repositorios.Interfaces;
using Huachin.MusicStore.Servicio.Interfaces;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Servicio.Implementaciones
{
    public class SaleServicio : ISaleServicio
    {
        private readonly ISaleRepositorio _saleRepositorio;
        private readonly ILogger<SaleServicio> _logger;

		public SaleServicio(ISaleRepositorio saleRepositorio, ILogger<SaleServicio> logger)
		{
			_saleRepositorio = saleRepositorio;
			_logger = logger;
		}

		public async Task<BaseResponse> Registrar(SaleRequestDto request)
		{
			var response = new BaseResponse();
			try
			{
				var sale = new Sale()
				{
					IdCustomer = request.IdCustomer,
					IdConcert = request.IdConcert,
					SaleDate = DateTime.Now.Date,
					OperationNumber = request.OperationNumber,
					Total = request.Total,
					Quantity = request.Quantity
				};

				await _saleRepositorio.AddAsync(sale);

				response.Message = "Venta registrado correctamente";
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Message = "Ocurrió un error al registrar una venta";
				response.Success = false;
				_logger.LogError(ex, $"{response.Message} {ex.Message}");
			}

			return response;
		}

	}
}
