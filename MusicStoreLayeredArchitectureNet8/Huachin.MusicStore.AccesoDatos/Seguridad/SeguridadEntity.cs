using Microsoft.AspNetCore.Identity;

namespace Huachin.MusicStore.AccesoDatos.Seguridad
{
    public class SeguridadEntity : IdentityUser
    {
		public int IdUsuario { get; set; }
		public string NombreCompleto { get; set; } = default!;
	}
}
