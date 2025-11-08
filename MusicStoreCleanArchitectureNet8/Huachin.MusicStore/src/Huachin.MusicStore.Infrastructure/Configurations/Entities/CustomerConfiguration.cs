using Huachin.MusicStore.Domain.Entities.MusicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huachin.MusicStore.Infrastructure.Configurations.Entities
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("Customers");

			builder.HasKey(c => c.Id);
			
			builder.Property(c => c.FirstName)
				.IsRequired()
				.HasMaxLength(50);
			
			builder.Property(c => c.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.OwnsOne(c => c.Email, email =>
			{
				email.Property(m => m.Value)
					.HasColumnName("Email")
					.IsRequired()
					.HasMaxLength(100); ;				
			});
		}
	}
}
