export const GetAllMoviesQuery = `query AllMovies{
    listMovies{
        name
        description
        genres
        rate
        length
    }
}`;

export const GetTop5MoviesQuery = `
    query Top5Movies {

        top5 {
            id
            name
            genres
            rate
            description    
        }
    }
`;

export const GetMoviesByGenreQuery = `
query MoviesByGenre ($genre:String!){
    moviesByGenre(genre: $genre){
        id
        name
        genres
        rate
        description
        length  
    }
}
`;

export const SearchMoviesQuery = `
query Search ($phrase:String!){
    search(phrase: $phrase){
        id
        name
        genres
        rate
        description
        length   
    }
}
`;
