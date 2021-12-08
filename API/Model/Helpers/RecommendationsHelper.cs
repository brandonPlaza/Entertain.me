using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace API.Model.Helpers
{
    public static class RecommendationsHelper
    {
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

       
    }
}