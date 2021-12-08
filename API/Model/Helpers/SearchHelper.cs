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
    public class SearchHelper
    {
        private readonly string _key;
        private readonly HttpClient _client = new HttpClient();
        public SearchHelper(string key)
        {
            _key = key;
        }

       public static IEnumerable<string> GetAllMediaByTitle(string mediaTitle){
           string apiKey = "6bfc5edb70cf2f059fcfc2b4517f89dd";
           var client = new RestClient("https://api.themoviedb.org/3/search/multi");
           client.Timeout = -1;
           var request = new RestRequest(Method.GET);
           request.AddParameter("api_key",apiKey);
           request.AddParameter("query",mediaTitle);
           IRestResponse response = client.Execute(request);
           string[] rawResponse = response.Content.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
           foreach(string responseString in rawResponse){
               yield return responseString.Replace("\"", string.Empty);
           }
       }

       public async Task<List<Media>> SearchForMedia(DataContext db, string title)
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