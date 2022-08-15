using Microsoft.AspNetCore.Mvc;
using Movies.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Server.Controllers
{
	[Route("api/[controller]")]
	public class MoviesDataController : Controller
	{
		private readonly IMovieGrainClient _client;

		public MoviesDataController(
			IMovieGrainClient client
		)
		{
			_client = client;
		}

		// GET api/sampledata/1234
		[HttpGet("{id}")]
		public async Task<Movie> Get(int id)
		{
			var result = await _client.Get(id).ConfigureAwait(false);
			return result;
		}

		[HttpGet]
		public async Task<List<Movie>> GetAll()
		{
			var result = await _client.GetAll().ConfigureAwait(false);
			return result;
		}

		/*
		// POST api/sampledata/1234
		[HttpPost("{id}")]
		public async Task Set([FromRoute] string id, [FromForm] string name)
			=> await _client.Set(id, name).ConfigureAwait(false);
		*/
		/*
		[HttpPost]
		public async Task Set([FromBody] Movie movie)
			=> await _client.Set(movie).ConfigureAwait(false);
		*/
	}
}