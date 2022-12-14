using Movies.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Server
{
	public static class Utilities
	{
		public static async Task LoadMoviesIntoGrain(IMovieGrainClient grainClient)
		{
			using (StreamReader file = File.OpenText(@"./Data/" + "movies.json"))
			{
				string s = file.ReadToEnd();

				List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(s);

				foreach (Movie movie in movies)
				{
					await grainClient.AddMovie(movie);
				}

			}
		}
	}
}

