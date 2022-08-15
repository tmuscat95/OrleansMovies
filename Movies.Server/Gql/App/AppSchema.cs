﻿using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Movies.Server.Gql.App
{
	public class AppSchema : Schema
	{
		public AppSchema(IServiceProvider provider)
			: base(provider)
		{
			Query = provider.GetRequiredService<MovieGraphQuery>();
			Mutation = provider.GetRequiredService<AppGraphMutation>();
		}
	}
}