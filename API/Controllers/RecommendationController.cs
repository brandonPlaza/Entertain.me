using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private MovieApiHelper _movieHelper;

        public RecommendationController(MovieApiHelper movieHelper)
        {
            _movieHelper = movieHelper;
        }

        [HttpGet]
        public async Task<IActionResult> RecommendMovies([FromQuery] List<string> movieIDs)
        {
            var results = await _movieHelper.RecommendMovies(movieIDs);
            return Ok(results);
        }
    }
}