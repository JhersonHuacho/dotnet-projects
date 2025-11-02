namespace Huachin.MusicStore.Dto.Response.Login
{
    public class LoginResponse
    {
		public int Id { get; set; }
		public string Email { get; set; } = default!;
		public string NombreCompleto { get; set; } = default!;
		public string Rol { get; set; } = default!;
	}
}
