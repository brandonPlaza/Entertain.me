using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model.Helpers
{
    /// <summary>
    /// Represents a movie from the API.
    /// </summary>
    public class Movie
    {
        [JsonPropertyName("adult")] public bool Adult { get; set; }
        [JsonPropertyName("genre_ids")] public List<int> GenreIds { get; set; } = new List<int>();
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("media_type")] public string MediaType { get; set; } = "";
        [JsonPropertyName("original_language")] public string Language { get; set; } = "";
        [JsonPropertyName("title")] public string Title { get; set; } = "";
        [JsonPropertyName("overview")] public string Overview { get; set; } = "";
    }
    
    /// Movie recommendation API helper
    public class MovieApiHelper
    {
        private readonly string _key;
        private readonly HttpClient _client = new HttpClient();
        
        public MovieApiHelper(string key)
        {
            _key = key;
        }

        /// Recommend new movies based on the existing list of favourites.
        public async Task<List<Movie>> RecommendMovies(List<Movie> favourites)
        {
            var allRecommendations = new List<Movie>();

            foreach (var favourite in favourites)
            {
                var data = await _client.GetAsync($"https://api.themoviedb.org/3/movie/{favourite.Id}/recommendations?api_key={_key}");
                if (!data.IsSuccessStatusCode)
                    throw new HttpRequestException("API Failure: " + await data.Content.ReadAsStringAsync());
                
                
                var result = await data.Content.ReadFromJsonAsync<MovieSearchResults>();
                if (result == null)
                    throw new HttpRequestException("Failed to parse API result");

                allRecommendations.AddRange(result!.Results);
            }

            return allRecommendations.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        /// Search for movies based on a title.
        public async Task<List<Movie>> SearchForMovies(string title)
        {
            var data = await _client.GetAsync($"https://api.themoviedb.org/3/search/multi?api_key={_key}&query=" +
                             UrlEncoder.Default.Encode(title));
            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException("API Failure: " + await data.Content.ReadAsStringAsync());

            var result = await data.Content.ReadFromJsonAsync<MovieSearchResults>();
            if (result == null)
                throw new HttpRequestException("Failed to parse API result");

            return result!.Results;
        }
        
        private class MovieSearchResults
        {
            [JsonPropertyName("results")] public List<Movie> Results { get; set; }
        }
    }
}