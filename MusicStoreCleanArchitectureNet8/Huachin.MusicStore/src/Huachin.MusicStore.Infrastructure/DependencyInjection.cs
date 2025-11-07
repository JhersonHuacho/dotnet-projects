using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;
using Huachin.MusicStore.Infrastructure.Persistence.Repositories;
using Huachin.MusicStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Huachin.MusicStore.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Register infrastructure services here
			services.AddDbContext<MusicStoreDbContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString("FlatFinderDb"));
			});

			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IConcertRepository, ConcertRepository>();
			services.AddScoped<ISaleRepository, SaleRepository>();
			services.AddScoped<IGenreRepository, GenreRespository>();

			services.AddScoped<IMusicStoreUnitOfWork, MusicStoreUnitOfWork>();

			return services;
		}
	}
}
