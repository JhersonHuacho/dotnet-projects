
using Huachin.MusicStore.Comun;
using System.ComponentModel.DataAnnotations;

namespace Huachin.MusicStore.Dto.Request.Login
{
    public class LoginRequest
    {
		[Required(ErrorMessage = Constantes.MensajeRequired)]
		public string Usuario { get; set; } = default!;

		[Required(ErrorMessage = Constantes.MensajeRequired)]
		public string Password { get; set; } = default!;
	}
}
