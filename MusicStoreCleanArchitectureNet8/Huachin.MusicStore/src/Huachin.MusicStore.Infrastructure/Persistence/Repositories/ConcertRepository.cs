using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Huachin.MusicStore.Infrastructure.Persistence.Repositories
{
	public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
	{
		public ConcertRepository(MusicStoreDbContext context) : base(context)
		{
		}

		public async Task<ICollection<Concert>> ListWithGenresAsync()
		{
			return await _context.Set<Concert>()
				.Include(c => c.Genre)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
