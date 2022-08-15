using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts
{
	public interface IMovieGrainClient
	{
		Task<Movie> Get(int id);
		//Task Set(string key, string name);
		Task Set(List<Movie> movies);
		Task<List<Movie>> GetAll();
	}
}
