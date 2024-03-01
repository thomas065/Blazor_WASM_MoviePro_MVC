using MoviePro.Models;
using System.Net.Http.Json;

namespace MoviePro.Services
{
	public class TMDBClient
	{
		private readonly HttpClient _httpClient;

		public TMDBClient(HttpClient httpClient, IConfiguration config)
		{
			_httpClient = httpClient;

			_httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/"); // the base url entry point for TMDB
			_httpClient.DefaultRequestHeaders.Accept.Add(new("application/json")); // hey, i'm an application and not a user looking for json in return

			string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found!!");
			_httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
		}
		public Task<PopularMoviePagedResponse?> GetPopularMoviesAsync()
		{
			return _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>("movie/popular");
		}

		public Task<MovieDetails?> GetMovieDetailsAsync(int id)
		{
			return _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
		}
	}

}
