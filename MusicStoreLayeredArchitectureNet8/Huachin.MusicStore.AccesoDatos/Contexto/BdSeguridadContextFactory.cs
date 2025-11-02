using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Huachin.MusicStore.AccesoDatos.Contexto
{
    public class BdSeguridadContextFactory : IDesignTimeDbContextFactory<BdSeguridadContext>
	{
		public BdSeguridadContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BdSeguridadContext>();
			optionsBuilder.UseNpgsql("Host=localhost:1404; Database=BdSeguridad; Username=admin; Password=password2024;");

			return new BdSeguridadContext(optionsBuilder.Options);
		}
	}
}
