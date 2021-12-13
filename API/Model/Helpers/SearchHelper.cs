using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Model.Entities;
using API.Model.Persistence;
using RestSharp;

namespace API.Model.Helpers
{
    public static class SearchHelper
    {
        private static readonly string _key = "6bfc5edb70cf2f059fcfc2b4517f89dd";
        private static readonly HttpClient _client = new HttpClient();
        
       public static async Task<List<Media>> SearchForMedia(DataContext db, string title)
        {
            var data = await _client.GetAsync($"https://api.themoviedb.org/3/search/multi?api_key={_key}&query=" +
                             UrlEncoder.Default.Encode(title));
            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException("API Failure: " + await data.Content.ReadAsStringAsync());

            var result = await data.Content.ReadFromJsonAsync<MediaSearchResults>();
            
            if (result == null)
                throw new HttpRequestException("Failed to parse API result");

            await db.Media.AddRangeAsync(result.Results);

            return result!.Results;
        }
        private class MediaSearchResults
        {
            [JsonPropertyName("results")] public List<Media> Results { get; set; }
        }
    }
}