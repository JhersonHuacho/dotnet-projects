using Huachin.MusicStore.AccesoDatos.Seguridad;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huachin.MusicStore.AccesoDatos.Contexto
{
    public class BdSeguridadContext : IdentityDbContext<SeguridadEntity>
    {
		public BdSeguridadContext(DbContextOptions<BdSeguridadContext> options) : base(options)
		{

		}		
	}
}
