using Huachin.MusicStore.Dto.Request.Generos;
using Huachin.MusicStore.Dto.Response;
using Huachin.MusicStore.Dto.Response.Generos;

namespace Huachin.MusicStore.Servicio.Interfaces
{
    public interface IGenreServicio
    {
        Task<BaseResponse> Registrar(GenreRequestDto request);
		Task<BaseResponseGeneric<IEnumerable<GenreResponseDto>>> GetAsyn();
		Task<BaseResponseGeneric<GenreResponseDto>> GetAsyn(int id);
		Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request);
		Task<BaseResponse> DeleteAsync(int id);
		Task<BaseResponseGeneric<IEnumerable<ListaClientesResponseDto>>> Listar(BusquedaGenerosRequest request);
	}
}
