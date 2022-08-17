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
		private List<int> _ids = new List<int>();
		/*In production, grain identifiers would be persisted in an external storage. 
		 In this example app, data is reloaded every time the app is started, so this is enough.*/
		public List<int> GetIds() { return _ids; } 
		public MovieGrainClient(
			IGrainFactory grainFactory
		)
		{
			_grainFactory = grainFactory;
		}
		public async Task<Movie> AddMovie(Movie movie)
		{	var id = movie.Id;
			
			if (_ids.Contains(id))
			{
				id = _ids.Max() + 1;
			}

			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			await grain.SetState(movie);
			_ids.Add(id);
			return await grain.GetState();
		}

		public Task<Movie> Get(int id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			return grain.GetState();
		}
		
		public async Task<List<Movie>> GetAll()
		{
			var movies = new List<Movie>();
			foreach(int i in _ids)
			{
				var grain = _grainFactory.GetGrain<IMovieGrain>(i);
				var movie = await grain.GetState();
				movies.Add(movie);
			}

			return movies;
		}

		public async Task<List<Movie>> GetMoviesByGenre(string genre)
		{
			var movies = await GetAll();
			return movies.Where(m => m.Genres.Select(g => g.ToLower()).Contains(genre.Trim().ToLower()))
				.Select(m => m).ToList();
		}

		public async Task<List<Movie>> GetTop5Movies()
		{
			var movies = await GetAll();
			return movies.OrderByDescending(m => float.Parse(m.Rate)).Take(5).ToList();
		}

		public async Task<List<Movie>> SearchMovies(string phrase)
		{
			var movies = await GetAll();
			return movies
				.Where(m => m.Name.ToLower()
				.Contains(phrase.ToLower()) || m.Description.ToLower().Contains(phrase.ToLower()))
				.Select(m=>m)
				.ToList();
		}
	
		public async Task<Movie> UpdateMovie(int id,Movie movie)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			await grain.SetState(movie);
			return await grain.GetState();

		}

		
	}
}