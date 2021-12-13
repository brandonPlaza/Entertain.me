using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model.DTOs
{
    public class MediaDTO
    {
        [JsonPropertyName("id")] public int Id { get; set; } 
        [JsonPropertyName("adult")] public bool Adult { get; set; }

        // [NotMapped][JsonPropertyName("genre_ids")] public List<int> GenreIds { get; set; } = new List<int>();
        [JsonPropertyName("media_type")] public string MediaType { get; set; } = "";
        [JsonPropertyName("original_language")] public string Language { get; set; } = "";
        [JsonPropertyName("title")] public string Title { get; set; } = "";
        [JsonPropertyName("overview")] public string Overview { get; set; } = "";
    }
}