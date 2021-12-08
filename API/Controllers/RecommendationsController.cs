using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.Entities;
using API.Model.Helpers;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public RecommendationsController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMediaByTitle([FromQuery] string mediaTitle)
        {
            var response = ReccomendationsHelper.GetAllMediaByTitle(mediaTitle);
            return Ok(response);
        }
    }
}