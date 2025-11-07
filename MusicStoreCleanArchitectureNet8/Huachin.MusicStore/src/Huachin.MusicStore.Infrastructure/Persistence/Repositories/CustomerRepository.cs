using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;

namespace Huachin.MusicStore.Infrastructure.Persistence.Repositories
{
	public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(MusicStoreDbContext context) : base(context)
		{
		}
	}
}
