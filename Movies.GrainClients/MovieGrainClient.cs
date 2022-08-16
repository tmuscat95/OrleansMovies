using Movies.Contracts;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.GrainClients
{
	public class MovieGrainClient : IMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieGrainClient(
			IGrainFactory grainFactory
		)
		{
			_grainFactory = grainFactory;
		}

		public Task<Movie> Get(int id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			return grain.Get(id);
		}

		public Task<List<Movie>> GetAll()
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			return grain.GetState();
		}

		public async Task<List<Movie>> GetMoviesByGenre(string genre)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			var movies = await grain.GetState();
			return movies.Where(m => m.Genres.Select(g => g.ToLower()).Contains(genre.Trim().ToLower()))
				.Select(m => m).ToList();
		}

		public async Task<List<Movie>> GetTop5Movies()
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			var movies = await grain.GetState();
			return movies.OrderByDescending(m => float.Parse(m.Rate)).Take(5).ToList();
		}

		public async Task<List<Movie>> SearchMovies(string phrase)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			var movies = await grain.GetState();
			return movies
				.Where(m => m.Name.ToLower()
				.Contains(phrase.ToLower()) || m.Description.ToLower().Contains(phrase.ToLower()))
				.Select(m=>m)
				.ToList();
		}

		public Task Set(List<Movie> movies)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			return grain.SetState(movies.ToList());
		}

		public async Task<Movie> UpdateMovie(int id,Movie movie)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			var movies = await grain.GetState();
			var movieToUpdate = movies.Where(m => m.Id == id).Select(m => m).FirstOrDefault();
			movieToUpdate.Update(movie);
			await grain.SetState(movies);
			return movieToUpdate;

		}

		public async Task<Movie> CreateMovie(Movie movie)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			var movies = await grain.GetState();
			movie.Id = movies.Select(m => m.Id).Max() + 1;
			movies.Add(movie);
			await grain.SetState(movies);
			return movie;
		}
	}
}