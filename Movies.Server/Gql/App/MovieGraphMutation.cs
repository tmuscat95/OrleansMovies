using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Movies.Contracts;
using Movies.Server.Gql.Types;
using System;

namespace Movies.Server.Gql.App
{
	public class MovieGraphMutation : ObjectGraphType
	{
		public MovieGraphMutation(IServiceProvider provider)
		{
			Name = "MovieMutations";
			var movieClient = provider.GetRequiredService<IMovieGrainClient>();

			var movieArgument = new QueryArgument<MovieInputType> { Name = "movie", Description = "Updated Movie Object" };
			var nameArgument = new QueryArgument<IntGraphType> { Name = "id", Description = "Id Of Movie To Update" };
			Field<MovieGraphType>("updateMovie",
				arguments: new QueryArguments(nameArgument, movieArgument),
				resolve: context => { return movieClient.UpdateMovie(context.GetArgument<int>("id"), context.GetArgument<Movie>("movie")); });

			Field<MovieGraphType>("createMovie",
				arguments: new QueryArguments(movieArgument),
				resolve: context => movieClient.AddMovie(context.GetArgument<Movie>("movie")));
		}
	}
}