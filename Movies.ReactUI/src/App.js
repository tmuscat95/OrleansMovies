import React, { useState, useCallback } from "react";
import { createClient } from "urql";
import {
	GetAllMoviesQuery,
	GetMoviesByGenreQuery,
	GetTop5MoviesQuery,
	SearchMoviesQuery,
} from "./queries";
import "./App.css";

const App = (props) => {
	const [movies, setMovies] = useState([]);
	const [loading, setLoading] = useState(false);
	const [genre, setGenre] = useState("");
	const [searchTerm, setSearchTerm] = useState("");
	const client = createClient({
		url: "/api/graphql",
	});
	/*
	useEffect(() => {
		getAllMovies();
	}, []);
	//const displayName = useRef(App.name);
	const getAllMovies = useCallback(async () => {
		const response = await fetch("/api/moviesdata");
		const data = await response.json();
		setMovies(data);
		setLoading(false);
	});
*/	

	const getMovies = async (queryString, vars = {}) => {
		setLoading(true);
		const result = await client.query(queryString, vars).toPromise();
		const { data } = result;
		setLoading(false);
		setMovies(data[Object.keys(data)[0]]);
	};

	const renderMoviesTable = useCallback(() => {
		return (
			<table className="table table-striped" aria-labelledby="tabelLabel">
				<thead>
					<tr>
						<th>Name</th>
						<th>Description</th>
						<th>Genres</th>
						<th>Rate</th>
						<th>Length</th>
					</tr>
				</thead>
				<tbody>
					{movies.map((movie) => (
						<tr key={movie.id}>
							<td>{movie.name}</td>
							<td>{movie.description}</td>
							<td>{movie.genres.toString()}</td>
							<td>{movie.rate}</td>
							<td>{movie.length}</td>
						</tr>
					))}
				</tbody>
			</table>
		);
	});

	let buttonStyle = {
		margin: "5px",
	};

	let divStyle = { padding: "5px" };

	let contents = loading ? (
		<p>
			<em>
				Loading... Please refresh once the ASP.NET backend has started. See{" "}
				<a href="https://aka.ms/jspsintegrationreact">
					https://aka.ms/jspsintegrationreact
				</a>{" "}
				for more details.
			</em>
		</p>
	) : (
		renderMoviesTable(movies)
	);

	let genres = ["scifi", "action", "crime", "drama", "adventure", "comedy"];
	return (
		<div>
			<h1 id="tabelLabel">Movies</h1>
			<p>Movies GraphQL API Basic FrontEnd</p>
			<div style={divStyle}>
				<button
					style={buttonStyle}
					onClick={getMovies.bind(this, GetAllMoviesQuery, {})}
				>
					Get All Movies
				</button>
				<button
					style={buttonStyle}
					onClick={getMovies.bind(this, GetTop5MoviesQuery, {})}
				>
					Get Top 5 Movies
				</button>
			</div>
			<div style={divStyle}>
				<label for="genres">Genres: </label>
				<select
					name="genres"
					id="genres"
					onChange={(e) => {
						e.preventDefault();
						setGenre(e.target.value);
					}}
				>
					{genres.map((genre) => {
						return <option value={genre}>{genre}</option>;
					})}
				</select>
				<button
					style={buttonStyle}
					onClick={getMovies.bind(this, GetMoviesByGenreQuery, {
						genre: genre,
					})}
				>
					Get By Genre
				</button>
			</div>
			<div style={divStyle}>
				<label for="search">Search: </label>
				<input
					type="text"
					placeholder="Search By Name and Description..."
					onChange={(e) => {
						e.preventDefault();
						setSearchTerm(e.target.value);
					}}
				/>
				<button
					onClick={getMovies.bind(this, SearchMoviesQuery, { phrase: searchTerm })}
				>
					Search
				</button>
			</div>

			{contents}
		</div>
	);
};

export default App;
