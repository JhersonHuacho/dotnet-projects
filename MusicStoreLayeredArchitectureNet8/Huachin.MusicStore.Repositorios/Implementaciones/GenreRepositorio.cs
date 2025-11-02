using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Repositorios.Interfaces;

namespace Huachin.MusicStore.Repositorios.Implementaciones
{
	public class GenreRepositorio : RepositorioBase<Genre>, IGenreRepositorio
	{
		private readonly MusicStoreContext _musicStoreContext;
		public GenreRepositorio(MusicStoreContext musicStoreContext) : base(musicStoreContext)
		{
			_musicStoreContext = musicStoreContext;
		}
	}
}
