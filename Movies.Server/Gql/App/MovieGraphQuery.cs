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

			Field<ListGraphType<MovieGraphType>>("movies", resolve: context => movieClient.GetAll());

			Field<MovieGraphType>("movie", arguments: new QueryArguments(new QueryArgument<StringGraphType>
			{
				Name = "id"
			}), resolve: context => movieClient.Get(int.Parse(context.Arguments["id"].ToString().Trim())));
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
