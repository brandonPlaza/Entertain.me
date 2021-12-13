using System;

namespace API.Model.Entities
{
    public class UserGenre
    {
        /// <summary>
        /// The ID of the genre on the third-party Movie API.
        /// </summary>
        public int GenreId { get; set; }
        
        public string GenreName { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}