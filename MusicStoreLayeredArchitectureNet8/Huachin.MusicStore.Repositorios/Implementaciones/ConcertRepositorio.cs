using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Repositorios.Interfaces;

namespace Huachin.MusicStore.Repositorios.Implementaciones
{
	public class ConcertRepositorio : RepositorioBase<Concert>, IConcertRepositorio
	{
		public ConcertRepositorio(MusicStoreContext musicStoreContext) : base(musicStoreContext)
		{
		}
	}
}
