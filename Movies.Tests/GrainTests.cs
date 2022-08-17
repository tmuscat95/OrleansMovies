using Movies.Contracts;
using Movies.GrainClients;
using Orleans.TestingHost;
using Orleans.Hosting;

namespace Tests
{
    public class Tests
    {
		private class SiloConfigurator : ISiloConfigurator
		{
			public void Configure(ISiloBuilder hostBuilder) =>
			   hostBuilder.AddMemoryGrainStorage("Default");
		}

		TestCluster cluster;
		IMovieGrainClient movieGrainClient;
        [SetUp]
        public async Task Setup()
        {
			var builder = new TestClusterBuilder();
			builder.AddSiloBuilderConfigurator<SiloConfigurator>();
			cluster = builder.Build();
			cluster.Deploy();
			movieGrainClient = new MovieGrainClient(cluster.GrainFactory);
			//await Utilities.LoadMoviesIntoGrain(movieGrainClient);
			
		}

		[TearDown]
		public void TearDown()
		{
			cluster.Dispose();
		}

		[Test]
		[Description("Tests that grain state is set and persisted properly.")]
        public async Task TestGrains()
        {
			var movies = new List<Movie>();
			for (int i = 0; i < 3; i++)
			{
				var grain = cluster.GrainFactory.GetGrain<IMovieGrain>(i);
				var movie = new Movie() { Id = i, Name = $"test{i}", Description = $"test{i}", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = $"testimageurl{i}", Key = $"test-key-{i}", Length = $"test_length{i}", Rate = $"{i}" };
				movies.Add(movie);
				await grain.SetState(movie);
			}

			for(int i = 0; i < 3; i++)
			{
				var grain = cluster.GrainFactory.GetGrain<IMovieGrain>(i);
				var grainState = await grain.GetState();
				Assert.IsTrue(movies[i].Equals(grainState));
			}
        }

		[Test]
		[Description("Tests that new movie is successfully added, and that the index of ids is modified accordingly")]
		public async Task TestAddMovie()
		{
			var moviesCountBefore = (await movieGrainClient.GetAll()).Count;
			await movieGrainClient.AddMovie(new Movie() { Id=0, Name = "test", Description = "test", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" });
			var moviesCountAfter = (await movieGrainClient.GetAll()).Count;
			Assert.IsTrue((moviesCountAfter - moviesCountBefore) == 1);
		}

		[Test]
		public async Task TestGet()
		{
			var movie = new Movie() { Id = 77, Name = "test", Description = "test", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
			await movieGrainClient.AddMovie(movie);
			var gotMovie = await movieGrainClient.Get(77);
			Assert.IsTrue(movie.Equals(gotMovie));
			
		}

		[Test]
		public async Task TestGetAll()
		{
			var movies = new List<Movie>();
			for (int i = 0; i < 3; i++)
			{
				var movie = new Movie() { Id = i, Name = $"test{i}", Description = $"test{i}", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = $"testimageurl{i}", Key = $"test-key-{i}", Length = $"test_length{i}", Rate = $"{i}" };
				movies.Add(movie);
				await movieGrainClient.AddMovie(movie);
			}

			var gotMovies = await movieGrainClient.GetAll();
			for (int i = 0; i < 3; i++)
			{
				Assert.IsTrue(movies[i].Equals(gotMovies[i]));
			}

		}

		[Test]
		public async Task TestGetMoviesByGenre()
		{
			var movie1 = new Movie() { Id = 0, Name = "test", Description = "test", Genres = new List<string>(new string[] { "scifi", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
			var movie2 = new Movie() { Id = 1, Name = "test", Description = "test", Genres = new List<string>(new string[] { "scifi", "thriller" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
		
			await movieGrainClient.AddMovie(movie1);
			await movieGrainClient.AddMovie(movie2);

			var searchResultsSciFi = await movieGrainClient.GetMoviesByGenre("scifi");
			var searchResultsFantasy = await movieGrainClient.GetMoviesByGenre("fantasy");
			Assert.IsTrue(searchResultsSciFi.Contains(movie1) && searchResultsSciFi.Contains(movie2));
			Assert.IsTrue(searchResultsFantasy.Contains(movie1) && !searchResultsFantasy.Contains(movie2));
		}

		[Test]
		[Description("Tests to make sure the Search function can find keywords in the movie name and description")]
		public async Task TestSearchMovies()
		{
			var searchTerm = "TEST_SEARCH_TERM";
			var movie1 = new Movie() { Id = 0, Name = $"TestName {searchTerm}", Description = "test", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
			var movie2 = new Movie() { Id = 0, Name = $"TestName", Description = $"test {searchTerm}", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
			await movieGrainClient.AddMovie(movie2);
			await movieGrainClient.AddMovie(movie1);

			var gotMovies = (await movieGrainClient.SearchMovies(searchTerm));
			Assert.IsTrue(gotMovies.Contains(movie1) && gotMovies.Contains(movie2));

		}

		[Test]
		public async Task TestUpdateMovie()
		{
			int id = 77;
			var movie1 = new Movie() { Id = id, Name = "test", Description = "test", Genres = new List<string>(new string[] { "scifi", "horror", "fantasy" }), Img = "testimageurl", Key = "test-key", Length = "test_length", Rate = "9" };
			var movie2 = (Movie) movie1.Clone();
			movie2.Name = "updatedName";
			await movieGrainClient.AddMovie(movie1);
			var updatedMovie = await movieGrainClient.UpdateMovie(id, movie2);
			Assert.True(updatedMovie.Equals(movie2));

			
		}
	}
}