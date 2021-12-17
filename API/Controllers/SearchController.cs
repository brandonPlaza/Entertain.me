using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SearchController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        public SearchController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, DataContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
            this._context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMediaByTitle([FromQuery] string mediaTitle)
        {
            var response = await SearchHelper.SearchForMedia(_context,mediaTitle);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("addToFavourites")]
        public async Task<IActionResult> AddToUserFavourites([FromBody] List<string> mediaTitles)
        {
            return Ok();
        }
    }
}