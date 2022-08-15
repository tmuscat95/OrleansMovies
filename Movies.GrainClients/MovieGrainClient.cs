using Movies.Contracts;
using Orleans;
using System.Collections.Generic;
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
			return grain.GetAll();
		}
		/* 
		public Task Set(string key, string name)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(key);
			return grain.Set(name);
		}
		*/
		public Task Set(List<Movie> movies)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>("movies");
			return grain.Set(movies);
		}
	}
}