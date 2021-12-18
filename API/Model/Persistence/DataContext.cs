using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Media> Favourites { get; set; } // table of media items 

        public DbSet<Media> WatchList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=data.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGenre>()
                .HasKey(g => new { g.UserId, g.GenreId});
            base.OnModelCreating(builder);
        }
        
    }
}