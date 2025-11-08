namespace Huachin.MusicStore.Application.Dtos.Sales
{
	public class ListaVentasResponseDto
	{
		public Guid IdSale { get; set; }
		public Guid IdConcert { get; set; }
		public string TitleEvento { get; set; } = default!;
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime SaleDate { get; set; }
	}
}
