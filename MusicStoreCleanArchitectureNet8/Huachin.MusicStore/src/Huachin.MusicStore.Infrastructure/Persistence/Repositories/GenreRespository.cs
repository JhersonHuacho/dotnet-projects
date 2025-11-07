using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;

namespace Huachin.MusicStore.Infrastructure.Persistence.Repositories
{
	public class GenreRespository : BaseRepository<Genre>, IGenreRepository
	{
		public GenreRespository(MusicStoreDbContext context) : base(context)
		{
		}
	}
}
