using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts
{
	public interface IMovieGrain : IGrainWithStringKey
	{
		Task<Movie> Get(int id);
		Task<List<Movie>> GetState();
		Task SetState(List<Movie> name);
	}
}