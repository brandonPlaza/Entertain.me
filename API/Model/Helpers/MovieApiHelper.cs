using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Model.Entities;
using API.Model.Persistence;

namespace API.Model.Helpers
{
    /// Movie recommendation API helper
    public class MovieApiHelper
    {
        private readonly string _key;
        private readonly HttpClient _client = new HttpClient();
        
        public MovieApiHelper(string key)
        {
            _key = key;
        }

        /// Recommend new movies based on the existing list of favourite movie IDs.
        public async Task<List<Media>> RecommendMovies(DataContext db, List<string> favouriteIDs)
        {
            var allRecommendations = new List<Media>();

            foreach (var favourite in favouriteIDs)
            {
                var data = await _client.GetAsync($"https://api.themoviedb.org/3/movie/{favourite}/recommendations?api_key={_key}");
                if (!data.IsSuccessStatusCode)
                    throw new HttpRequestException("API Failure: " + await data.Content.ReadAsStringAsync());
                
                
                var result = await data.Content.ReadFromJsonAsync<MovieSearchResults>();
                if (result == null)
                    throw new HttpRequestException("Failed to parse API result");

                allRecommendations.AddRange(result!.Results);
            }

            var unique = allRecommendations.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            await db.Favourites.AddRangeAsync(unique);
            return unique;
        }
        
        private class MovieSearchResults
        {
            [JsonPropertyName("results")] public List<Media> Results { get; set; }
        }
    }
}