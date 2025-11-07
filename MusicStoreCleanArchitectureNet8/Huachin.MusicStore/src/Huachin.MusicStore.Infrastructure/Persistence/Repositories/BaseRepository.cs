using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Huachin.MusicStore.Infrastructure.Persistence.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
		where TEntity : BaseEntity
	{
		protected readonly MusicStoreDbContext _context;

		public BaseRepository(MusicStoreDbContext context)
		{
			_context = context;
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			var result = await _context.Set<TEntity>().AddAsync(entity);

			return result.Entity;
		}

		public void Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
		}

		public async Task<TEntity?> GetByIdAsync(Guid id)
		{
			return await _context.Set<TEntity>()
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<ICollection<TEntity>> ListAsync()
		{
			return await _context.Set<TEntity>()
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
