using Huachin.MusicStore.Dto.Request.Sale;
using Huachin.MusicStore.Dto.Response;

namespace Huachin.MusicStore.Servicio.Interfaces
{
    public interface ISaleServicio
    {
		Task<BaseResponse> Registrar(SaleRequestDto request);
	}
}
