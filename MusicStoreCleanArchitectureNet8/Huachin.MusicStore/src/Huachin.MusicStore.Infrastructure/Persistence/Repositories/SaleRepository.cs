using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;

namespace Huachin.MusicStore.Infrastructure.Persistence.Repositories
{
	public class SaleRepository : BaseRepository<Sale>, ISaleRepository
	{
		public SaleRepository(MusicStoreDbContext context) : base(context)
		{
		}
	}
}
