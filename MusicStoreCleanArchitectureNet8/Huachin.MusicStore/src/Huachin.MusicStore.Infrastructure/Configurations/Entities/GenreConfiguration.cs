using Huachin.MusicStore.Domain.Entities.MusicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huachin.MusicStore.Infrastructure.Configurations.Entities
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.ToTable("Genres");

			builder.HasKey(g => g.Id);
			
			builder.Property(g => g.Name)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
