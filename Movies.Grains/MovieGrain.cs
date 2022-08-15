using Movies.Contracts;
using Orleans;
using Orleans.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	[StorageProvider(ProviderName = "Default")]
	public class MovieGrain : Grain<List<Movie>>, IMovieGrain
	{
		public Task<List<Movie>> GetAll()
			=> Task.FromResult(State);

		public Task<Movie> Get(int id) => Task.FromResult(State.Find(x => x.Id == id));
		public Task Set(List<Movie> movies)
		{
			//State = new SampleDataModel { Id = this.GetPrimaryKeyString(), Name = name };
			State = movies;
			return Task.CompletedTask;
		}
	}
}