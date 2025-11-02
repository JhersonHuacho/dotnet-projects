using Huachin.MusicStore.Domain.Exceptions;
using Huachin.MusicStore.Domain.ValueObjects;

namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class Customer : BaseEntity
	{
		public Email Email { get; private set; } = null!;
		public string FullName { get; private set; } = string.Empty;

		// Navigation property
		private readonly List<Sale> _sales = new();
		public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();

		public Customer()
		{
		}

		private Customer(Email email, string fullName)
		{
			Email = email;
			FullName = fullName;
		}

		public static Customer Create(Email email, string fullName)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new DomainException("Email cannot be null or empty.");
			}

			if (string.IsNullOrWhiteSpace(fullName))
			{
				throw new DomainException("Full name cannot be null or empty.");
			}

			if (!IsValidEmail(email))
			{
				throw new DomainException("Invalid email format.");
			}

			return new Customer(email, fullName);
		}

		private static bool IsValidEmail(string email)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
		}
	}
}
