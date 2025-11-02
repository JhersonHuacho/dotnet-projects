namespace Huachin.MusicStore.Domain.ValueObjects
{
	public record Money
	{
		public decimal Amount { get; init; }
		public string Currency { get; init; } = string.Empty;

		private Money() { } // For EF Core

		private Money(decimal amount, string currency)
		{
			if (amount < 0)
				throw new ArgumentException("Amount cannot be negative.", nameof(amount));

			if (string.IsNullOrWhiteSpace(currency))
				throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

			Amount = amount;
			Currency = currency.ToUpperInvariant();
		}

		public static Money Create(decimal amount, string currency)
		{
			return new Money(amount, currency);
		}

		public static Money operator +(Money left, Money right)
		{
			if (left.Currency != right.Currency)
				throw new InvalidOperationException("Cannot add amounts with different currencies.");

			if (left.Amount <= 0 || right.Amount <= 0)
				throw new InvalidOperationException("Cannot add amounts when one or both amounts are null.");

			return new Money(left.Amount + right.Amount, left.Currency!);
		}
	}
}
