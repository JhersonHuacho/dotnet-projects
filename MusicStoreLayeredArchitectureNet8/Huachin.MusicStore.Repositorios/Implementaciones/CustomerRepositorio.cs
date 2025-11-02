using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Repositorios.Interfaces;

namespace Huachin.MusicStore.Repositorios.Implementaciones
{
	public class CustomerRepositorio : RepositorioBase<Customer>, ICustomerRepositorio
	{
		public CustomerRepositorio(MusicStoreContext musicStoreContext) : base(musicStoreContext)
		{
		}
	}
}
