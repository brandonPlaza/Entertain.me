using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Model.DTOs;
using API.Model.Entities;
using API.Model.Persistence;
using RestSharp;

namespace API.Model.Helpers
{
    public static class SearchHelper
    {
        private static readonly string _key = "6bfc5edb70cf2f059fcfc2b4517f89dd";
        private static readonly HttpClient _client = new HttpClient();
        
       public static async Task<List<MediaDTO>> SearchForMedia(DataContext db, string title)
        {
            var data = await _client.GetAsync($"https://api.themoviedb.org/3/search/multi?api_key={_key}&query=" +
                             UrlEncoder.Default.Encode(title));
            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException("API Error: " + await data.Content.ReadAsStringAsync());

            var result = await data.Content.ReadFromJsonAsync<MediaSearchResults>();
            
            if (result == null)
                throw new HttpRequestException("Something went wrong with the API");

            await db.Media.AddRangeAsync(MediaHelper.ConvertListOfMediaDtoToMedia(result.Results));

            return result!.Results;
        }

        public static async Task<Media> SearchForSpecificTitle(int movieId){
            var data = await _client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={_key}");

            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException("API Failure: " + await data.Content.ReadAsStringAsync());

            var result = await data.Content.ReadFromJsonAsync<MediaDTO>();
            
            if (result == null)
                throw new HttpRequestException("Something went wrong with the API");

            return MediaHelper.ConvertMediaDtoToMedia(result!);
        }

        private class MediaSearchResults
        {
            [JsonPropertyName("results")] public List<MediaDTO> Results { get; set; }
        }

        private class SingleResult{
            [JsonPropertyName("results")] public List<Media> Result { get; set; }
        }
    }
}