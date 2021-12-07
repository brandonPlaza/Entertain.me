using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMediaByTitle([FromQuery] string mediaTitle)
        {
            //TODO: Implement Realistic Implementation
            var response = ReccomendationsHelper.GetAllMediaByTitle(mediaTitle);
            return Ok(response);
        }
    }
}