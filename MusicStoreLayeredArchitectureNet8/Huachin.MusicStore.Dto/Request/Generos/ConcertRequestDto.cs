namespace Huachin.MusicStore.Dto.Request.Generos
{
    public class ConcertRequestDto
    {
		public int IdGenre { get; set; }

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Place { get; set; } = null!;

		public decimal UnitPrice { get; set; }

		public string Fecha { get; set; } = default!;
		public string Hora { get; set; } = default!;

		public string? ImageUrl { get; set; }

		public int TicketsQuantity { get; set; }

		public bool Estado { get; set; }
	}
}
