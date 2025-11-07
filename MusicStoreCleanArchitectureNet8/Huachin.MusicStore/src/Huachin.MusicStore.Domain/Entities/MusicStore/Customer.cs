using Huachin.MusicStore.Domain.Exceptions;
using Huachin.MusicStore.Domain.ValueObjects;

namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class Customer : BaseEntity
	{
		public Email Email { get; private set; } = null!;
		public string FirstName { get; private set; } = string.Empty;
		public string LastName { get; private set; } = string.Empty;

		// Navigation property
		private readonly List<Sale> _sales = new();
		public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();

		public Customer()
		{
		}

		private Customer(Email email, string firstName, string lastName)
		{
			Email = email;
			FirstName = firstName;
			LastName = lastName;
		}

		public static Customer Create(Email email, string firstName, string lastName)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new DomainException("Email cannot be null or empty.");
			}

			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new DomainException("Full name cannot be null or empty.");
			}

			if (string.IsNullOrWhiteSpace(lastName)) {
				throw new DomainException("Last name cannot be null or empty.");
			}

			return new Customer(email, firstName, lastName);
		}
	}
}
