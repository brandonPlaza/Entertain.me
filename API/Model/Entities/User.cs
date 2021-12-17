using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Model.Entities
{
    public class User : IdentityUser
    {
        public List<Media> Favourites { get; set; } 
        public ICollection<UserGenre> FavouriteGenres { get; set; }
        public ICollection<Media> WatchList { get; set; }
    }
}