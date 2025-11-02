using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Repositorios.Interfaces;

namespace Huachin.MusicStore.Repositorios.Implementaciones
{
	public class SaleRepositorio : RepositorioBase<Sale>, ISaleRepositorio
	{
		public SaleRepositorio(MusicStoreContext musicStoreContext) : base(musicStoreContext)
		{
		}
	}
}
