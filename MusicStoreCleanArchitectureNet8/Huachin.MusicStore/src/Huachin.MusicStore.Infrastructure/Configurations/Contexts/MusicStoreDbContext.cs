using Huachin.MusicStore.Domain.Entities.MusicStore;
using Microsoft.EntityFrameworkCore;

namespace Huachin.MusicStore.Infrastructure.Configurations.Contexts
{
	public class MusicStoreDbContext : DbContext
	{
		public MusicStoreDbContext()
		{
			
		}

		public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base(options)
		{
			
		}

		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Genre> Genres => Set<Genre>();
		public DbSet<Concert> Concerts => Set<Concert>();
		public DbSet<Sale> Sales => Set<Sale>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicStoreDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
