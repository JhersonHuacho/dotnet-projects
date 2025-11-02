namespace Huachin.MusicStore.Domain.ValueObjects
{
	public record Email
	{
		public string Value { get; init; }
		private Email(string value)
		{			
			Value = value;
		}

		public static Email Create(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Email cannot be null or empty.");

			if (!IsValid(value))
				throw new ArgumentException("Invalid email format.");

			return new Email(value);
		}

		private static bool IsValid(string email)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
		}

		public static implicit operator string(Email email) => email.Value;
	}
}
