namespace Huachin.MusicStore.Application.Dtos.Genres
{
	public class GenreResponseDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public bool Status { get; set; } = true;
	}
}
