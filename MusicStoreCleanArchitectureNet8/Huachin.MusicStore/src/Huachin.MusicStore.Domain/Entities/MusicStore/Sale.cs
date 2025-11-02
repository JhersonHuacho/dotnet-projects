using Huachin.MusicStore.Domain.ValueObjects;

namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class Sale : BaseEntity
	{
		public int IdCustomer { get; private set; }

		public int IdConcert { get; private set; }

		public DateTime SaleDate { get; private set; }

		public string OperationNumber { get; private set; } = string.Empty;

		public Money Total { get; private set; }

		public short Quantity { get; private set; }

		// Navigation properties
		public Customer Customer { get; private set; } = null!;
		public Concert Concert { get; private set; } = null!;

		public Sale()
		{
		}

		private Sale(
			int idCustomer,
			int idConcert,
			DateTime saleDate,
			string operationNumber,
			Money total,
			short quantity)
		{
			IdCustomer = idCustomer;
			IdConcert = idConcert;
			SaleDate = saleDate;
			OperationNumber = operationNumber;
			Total = total;
			Quantity = quantity;
		}

		public static Sale Create(
			int idCustomer,
			int idConcert,
			DateTime saleDate,
			string operationNumber,
			decimal total,
			short quantity)
		{

			if (saleDate == default)
			{
				throw new ArgumentException("Sale date must be provided.", nameof(saleDate));
			}

			if (string.IsNullOrWhiteSpace(operationNumber))
			{
				throw new ArgumentException("Operation number cannot be null or empty.", nameof(operationNumber));
			}

			if (total <= 0)
			{
				throw new ArgumentException("Total amount must be greater than zero.", nameof(total));
			}

			if (quantity <= 0)
			{
				throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
			}

			return new Sale(
				idCustomer,
				idConcert,
				saleDate,
				operationNumber,
				total,
				quantity);
		}
	}
}
