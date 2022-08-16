using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts
{
	public interface IMovieGrain : IGrainWithIntegerKey
	{
		//Task<Movie> Get(int id);
		Task<Movie> GetState();
		Task SetState(Movie movie);
	}
}