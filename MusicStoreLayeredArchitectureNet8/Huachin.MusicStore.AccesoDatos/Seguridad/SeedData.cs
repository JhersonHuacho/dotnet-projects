using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Huachin.MusicStore.AccesoDatos.Seguridad
{
    public class SeedData
    {
		public static async Task Inicializar(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<SeguridadEntity>>();
			var rolManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string[] roles = { "Administrador", "Cliente" };

			foreach (var rol in roles)
			{
				if (!await rolManager.RoleExistsAsync(rol))
				{
					await rolManager.CreateAsync(new IdentityRole(rol));
				}
			}

			var admin = new SeguridadEntity()
			{
				IdUsuario = 0,
				NombreCompleto = "Administrador del Sistema",
				Email = "admin@gmail.com",
				UserName = "admin",
			};

			await userManager.CreateAsync(admin, "Password2025#");
			await userManager.AddToRoleAsync(admin, "Administrador");

			var cliente = new SeguridadEntity()
			{
				IdUsuario = 1,
				NombreCompleto = "Cliente 1",
				Email = "cliente@gmail.com",
				UserName = "cliente",
			};

			await userManager.CreateAsync(cliente, "Password2025#");
			await userManager.AddToRoleAsync(cliente, "Cliente");
		}
	}
}
