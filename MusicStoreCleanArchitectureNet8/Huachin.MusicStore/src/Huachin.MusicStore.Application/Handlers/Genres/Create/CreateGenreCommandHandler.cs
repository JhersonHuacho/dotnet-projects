using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Domain.Entities.MusicStore;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Huachin.MusicStore.Application.Handlers.Genres.Create
{
	public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Guid>
	{
		private readonly ILogger<CreateGenreCommandHandler> _logger;
		private readonly IGenreRepository _genreRepository;
		private readonly IMusicStoreUnitOfWork _musicStoreUnitOfWork;

		public CreateGenreCommandHandler(
			ILogger<CreateGenreCommandHandler> logger, 
			IGenreRepository genreRepository, 
			IMusicStoreUnitOfWork musicStoreUnitOfWork)
		{
			_logger = logger;
			_genreRepository = genreRepository;
			_musicStoreUnitOfWork = musicStoreUnitOfWork;
		}

		public async Task<Guid> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
		{
			var genre = Genre.Create(request.Name);
			var result = await _genreRepository.AddAsync(genre);
			if (result == null)
			{
				_logger.LogError("Error creating genre with name {GenreName}", request.Name);
				throw new ApplicationException("Error creating genre");
			}

			await _musicStoreUnitOfWork.SaveChangesAsync();
			
			return result.Id;
		}
	}
}
