using GraphQL.Types;

namespace Movies.Server.Gql.Types
{
	public class MovieInputType : InputObjectGraphType
	{
		public MovieInputType()
		{
			//Field<IntGraphType>("id");
			Field<StringGraphType>("name");
			Field<StringGraphType>("key");
			Field<StringGraphType>("description");
			Field<ListGraphType<StringGraphType>>("genres");
			Field<StringGraphType>("rate");
			Field<StringGraphType>("length");
			Field<StringGraphType>("img");
		}
	}
}
