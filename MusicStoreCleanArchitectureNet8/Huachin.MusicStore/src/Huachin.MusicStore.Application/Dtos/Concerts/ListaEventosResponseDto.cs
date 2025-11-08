namespace Huachin.MusicStore.Application.Dtos.Concerts
{
	public class ListaEventosResponseDto
	{
		public Guid IdEvento { get; set; }
		public string NameGenre { get; set; } = default!;

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Place { get; set; } = null!;

		public decimal UnitPrice { get; set; }

		public DateTime DateEvent { get; set; }

		public int TicketsQuantity { get; set; }
		public string ImageUrl { get; set; } = default!;
		public bool Estado { get; set; }
	}
}
