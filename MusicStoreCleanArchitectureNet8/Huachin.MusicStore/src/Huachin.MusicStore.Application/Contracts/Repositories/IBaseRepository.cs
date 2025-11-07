using Huachin.MusicStore.Domain.Entities.MusicStore;

namespace Huachin.MusicStore.Application.Contracts.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity
	{
		Task<TEntity> AddAsync(TEntity entity);
		void Update(TEntity entity);
		Task<TEntity?> GetByIdAsync(Guid id);
		Task<ICollection<TEntity>> ListAsync();
	}
}
