using Huachin.MusicStore.Domain.Entities.MusicStore;

namespace Huachin.MusicStore.Application.Contracts.Repositories
{
	public interface IConcertRepository : IBaseRepository<Concert>
	{
		Task<ICollection<Concert>> ListWithGenresAsync();
	}
}
