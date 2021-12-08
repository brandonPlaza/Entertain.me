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
        public MovieApiHelper MovieHelper { get; set; }

        [HttpGet]
        public async Task<IActionResult> RecommendMovies([FromQuery] List<string> movieIDs)
        {
            var results = await MovieHelper.RecommendMovies(movieIDs);
            return Ok(results);
        }
    }
}