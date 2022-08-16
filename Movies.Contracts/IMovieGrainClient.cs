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
		Task<List<Movie>> GetMoviesByGenre(string genre);
		Task<List<Movie>> GetTop5Movies();
		Task<List<Movie>> SearchMovies(string phrase);

		Task<Movie> UpdateMovie(int id, Movie movie);
		Task<Movie> CreateMovie(Movie movie);
	}
}
