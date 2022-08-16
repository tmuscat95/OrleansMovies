using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Movies.Contracts;
using Movies.Server.Gql.Types;
using System;

namespace Movies.Server.Gql.App
{
	public class MovieGraphQuery : ObjectGraphType
	{
		public MovieGraphQuery(IServiceProvider provider)
		{
			var movieClient = provider.GetRequiredService<IMovieGrainClient>();
			Name = "MovieQueries";

			Field<ListGraphType<MovieGraphType>>("listMovies", resolve: context => movieClient.GetAll());

			Field<MovieGraphType>("movieById", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "id"
			}), resolve: context => movieClient.Get(int.Parse(context.Arguments["id"].ToString().Trim())));

			Field<ListGraphType<MovieGraphType>>("moviesByGenre", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "genre"
			}), resolve: context => movieClient.GetMoviesByGenre(context.Arguments["genre"].ToString()));

			Field<ListGraphType<MovieGraphType>>("top5", resolve: context => movieClient.GetTop5Movies());

			Field<ListGraphType<MovieGraphType>>(name:"search", description: "Search By Title And Description", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "phrase"
			}), resolve: context => movieClient.SearchMovies(context.Arguments["phrase"].ToString()));
			/*
			Field<MovieGraphType>("sample",
				arguments: new QueryArguments(new QueryArgument<StringGraphType>
				{
					Name = "id"
				}),
				resolve: ctx => movieClient.Get(int.Parse(ctx.Arguments["id"].ToString()))
			);*/
		}
	}
}
