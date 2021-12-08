using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model.Helpers;
using API.Model.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private MovieApiHelper _movieHelper;
        private DataContext _db;

        public RecommendationController(MovieApiHelper movieHelper, DataContext db)
        {
            _movieHelper = movieHelper;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> RecommendMovies([FromQuery] List<string> movieIDs)
        {
            var results = await _movieHelper.RecommendMovies(_db, movieIDs);
            return Ok(results);
        }
    }
}