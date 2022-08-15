using GraphQL.Types;
using Movies.Contracts;

namespace Movies.Server.Gql.Types
{
	public class MovieGraphType : ObjectGraphType<Movie>
	{
		public MovieGraphType()
		{
			Name = "Movie";
			Description = "Movie data.";

			Field(x => x.Id, nullable: false).Description("Numerical ID");
			Field(x => x.Key, nullable: false).Description("Key");
			Field(x => x.Name).Description("Name.");
			Field(x => x.Description);
			Field(x => x.Genres);
			Field(x => x.Rate);
			Field(x => x.Length);
			Field(x => x.Img);
		}
	}
}