namespace Huachin.MusicStore.Dto.Response.Eventos
{
    public class ListaEventosResponseDto
    {
		public int IdEvento { get; set; }
		public string NameGenre { get; set; } = default!;

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Place { get; set; } = null!;

		public decimal UnitPrice { get; set; }

		public DateTime DateEvent { get; set; }

		public int TicketsQuantity { get; set; }
		public string ImageUrl { get; set; } = default!;
		public bool Estado { get; set; }

		//public bool Finalized { get; set; }
	}
}
