using Huachin.MusicStore.Domain.ValueObjects;

namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class Concert : BaseEntity
	{
		public Guid IdGenre { get; private set; }

		public string Title { get; private set; } = string.Empty;

		public string Description { get; private set; } = string.Empty;

		public string Place { get; private set; } = string.Empty;

		public Money UnitPrice { get; private set; }

		public DateTime DateEvent { get; private set; }

		public string? ImageUrl { get; private set; }

		public int TicketsQuantity { get; private set; }

		public bool Finalized { get; private set; }

		// Navigation property
		public Genre Genre { get; private set; } = null!;

		// Navigation property
		private readonly List<Sale> _sales = new();
		public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();

		public Concert()
		{

		}

		private Concert(
			Guid idGenre, 
			string title, 
			string description, 
			string place, 
			Money unitPrice, 
			DateTime dateEvent, 
			string? imageUrl, 
			int ticketsQuantity, 
			bool finalized)
		{
			IdGenre = idGenre;
			Title = title;
			Description = description;
			Place = place;
			UnitPrice = unitPrice;
			DateEvent = dateEvent;
			ImageUrl = imageUrl;
			TicketsQuantity = ticketsQuantity;
			Finalized = finalized;
		}

		public static Concert Create(
			Guid idGenre, 
			string title, 
			string description, 
			string place, 
			Money unitPrice, 
			DateTime dateEvent, 
			string? imageUrl, 
			int ticketsQuantity, 
			bool finalized)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new ArgumentException("Concert title cannot be null or empty.", nameof(title));
			}

			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentException("Concert description cannot be null or empty.", nameof(description));
			}

			if (string.IsNullOrWhiteSpace(place))
			{
				throw new ArgumentException("Concert place cannot be null or empty.", nameof(place));
			}

			if (ticketsQuantity < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(ticketsQuantity), "Tickets quantity cannot be negative.");
			}

			return new Concert(idGenre, title, description, place, unitPrice, dateEvent, imageUrl, ticketsQuantity, finalized);
		}
	}
}
