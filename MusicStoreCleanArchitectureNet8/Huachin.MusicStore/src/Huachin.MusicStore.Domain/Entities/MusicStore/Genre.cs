using Huachin.MusicStore.Domain.Exceptions;

namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class Genre : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;

		// Navigation properties can be added here, e.g., a collection of Concerts associated with this Genre.
		private readonly List<Concert> _concerts = new();
		public IReadOnlyCollection<Concert> Concerts => _concerts.AsReadOnly();

		public Genre()
		{
			
		}

		private Genre(string name)
		{
			Name = name;
		}

		public static Genre Create(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new DomainException("Genre name cannot be null or empty.");
			}

			return new Genre(name);
		}
	}
}
