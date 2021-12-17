using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model.Entities;
using API.Model.Helpers;
using API.Model.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private MovieApiHelper _movieHelper;
        private DataContext _db;
        private readonly UserManager<User> _userManager;


        public RecommendationController(MovieApiHelper movieHelper, DataContext db, UserManager<User> manager)
        {
            _movieHelper = movieHelper;
            _db = db;
            _userManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> RecommendMovies([FromQuery] List<string> movieIDs)
        {
            var results = await _movieHelper.RecommendMovies(_db, movieIDs);
            return Ok(results);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task FavouriteMovie([FromQuery] int movieId)
        {
            (await _userManager.GetUserAsync(HttpContext.User)).Favourites
                .Add(await _db.Favourites.FindAsync(new {Id = movieId}));
        }
    }
}