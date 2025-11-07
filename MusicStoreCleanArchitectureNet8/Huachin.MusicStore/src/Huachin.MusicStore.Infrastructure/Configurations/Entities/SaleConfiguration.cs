using Huachin.MusicStore.Domain.Entities.MusicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huachin.MusicStore.Infrastructure.Configurations.Entities
{
	public class SaleConfiguration : IEntityTypeConfiguration<Sale>
	{
		public void Configure(EntityTypeBuilder<Sale> builder)
		{
			builder.ToTable("Sales");
			
			builder.HasKey(s => s.Id);
			
			builder.Property(s => s.Quantity)
				.IsRequired();

			builder.OwnsOne(s => s.Total, money =>
			{
				money.Property(m => m.Amount)
					.HasColumnName("TotalPrice")
					.IsRequired();
				
				money.Property(m => m.Currency)
					.HasColumnName("Currency")
					.IsRequired()
					.HasMaxLength(3);
			});
			
			/*
			 * "Cada venta está asociada a un cliente específico, y cada cliente puede tener múltiples ventas. 
			 * La relación se establece a través del campo IdCustomer en la tabla Sales, 
			 * y es obligatorio que cada venta tenga un cliente asignado."
			 */
			builder.HasOne(sale => sale.Customer)
				.WithMany(customer => customer.Sales)
				.HasForeignKey(s => s.IdCustomer)
				.IsRequired();
			
			/*
			 * "Cada venta está asociada a un concierto específico, y cada concierto puede tener múltiples ventas. 
			 * La relación se establece a través del campo IdConcert en la tabla Sales, 
			 * y es obligatorio que cada venta tenga un concierto asignado."
			 */
			builder.HasOne(sale => sale.Concert)
				.WithMany(concert => concert.Sales)
				.HasForeignKey(s => s.IdConcert)
				.IsRequired();
		}
	}
}
