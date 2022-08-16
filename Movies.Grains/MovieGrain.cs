using Movies.Contracts;
using Orleans;
using Orleans.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	[StorageProvider(ProviderName = "Default")]
	public class MovieGrain : Grain<Movie>, IMovieGrain
	{
		public Task<Movie> GetState()
			=> Task.FromResult(State);
		public Task SetState(Movie movie)
		{
			State = movie;
			return Task.CompletedTask;
		}
	}
}