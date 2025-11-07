using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Dtos.Genres;
using MediatR;

namespace Huachin.MusicStore.Application.Handlers.Genres.GetAll
{
	public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, ICollection<GenreResponseDto>>
	{
		private readonly IGenreRepository _genreRepository;

		public GetAllGenreQueryHandler(IGenreRepository genreRepository)
		{
			_genreRepository = genreRepository;
		}

		public async Task<ICollection<GenreResponseDto>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
		{
			var genres = await _genreRepository.ListAsync();

			var genresResponse = genres.Select(genre => new GenreResponseDto
			{
				Id = genre.Id,
				Name = genre.Name
			}).ToList();

			return genresResponse;
		}
	}
}
