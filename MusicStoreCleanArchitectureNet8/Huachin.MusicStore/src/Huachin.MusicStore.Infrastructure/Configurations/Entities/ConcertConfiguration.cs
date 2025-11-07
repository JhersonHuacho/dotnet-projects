using Huachin.MusicStore.Domain.Entities.MusicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huachin.MusicStore.Infrastructure.Configurations.Entities
{
	public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
	{
		public void Configure(EntityTypeBuilder<Concert> builder)
		{
			builder.ToTable("Concerts");

			builder.HasKey(c => c.Id);
			
			builder.Property(c => c.Title)
				.IsRequired()
				.HasMaxLength(100);
			
			builder.Property(c => c.Description)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(c => c.Place)
				.IsRequired()
				.HasMaxLength(200);

			builder.OwnsOne(c => c.UnitPrice, money =>
			{
				money.Property(m => m.Amount)
					.HasColumnName("UnitPrice")
					.IsRequired();
				
				money.Property(m => m.Currency)
					.HasColumnName("Currency")
					.IsRequired()
					.HasMaxLength(3);
			});

			builder.Property(c => c.DateEvent)
				.IsRequired();

			builder.Property(c => c.ImageUrl)
				.HasMaxLength(200);

			builder.Property(c => c.TicketsQuantity)
				.IsRequired();

			builder.Property(c => c.Finalized)
				.IsRequired();

			/*
			 * "Cada concert pertenece exactamente a un género, y cada género puede tener múltiples concerts. 
			 * La relación se establece a través del campo IdGenre en la tabla Concerts, 
			 * y es obligatorio que cada concert tenga un género asignado."
			 */
			builder.HasOne(concert => concert.Genre)
				.WithMany(genre => genre.Concerts)
				.HasForeignKey(c => c.IdGenre)
				.IsRequired();
		}
	}
}
