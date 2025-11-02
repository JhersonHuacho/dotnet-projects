namespace Huachin.MusicStore.Entidades
{
    public class EntidadBase
    {
		public int Id { get; set; }

		public bool Estado { get; set; } = true;

		public DateTime FechaCreacion { get; set; } = DateTime.Now;

		public string UsuarioCreacion { get; set; } = Environment.UserName;

		public DateTime? FechaModificacion { get; set; }

		public string? UsuarioModificacion { get; set; }
	}
}
