using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Model.Entities;
using API.Model.Helpers;
using API.Model.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddToUserFavourites([FromBody] List<int> mediaIds)
        {
            var newMedia = new List<Media>();
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var userEntry = await _context.Users.Include(user => user.Favourites).SingleOrDefaultAsync(user => user.Id == authUser.Id);
            foreach(int id in mediaIds){
                var mediaObj = await SearchHelper.SearchForSpecificTitle(_context,id);
                newMedia.Add(mediaObj);
                userEntry.Favourites.Add(mediaObj);
            }
            await _context.Favourites.AddRangeAsync(newMedia);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("favourites")]
        public async Task<IActionResult> GetUserFavourites()
        {
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var userEntry = _context.Users.Include(user => user.Favourites).SingleOrDefault(user => user.Id == authUser.Id);
            var favourites = MediaHelper.ConvertMediaToMediaTitles(userEntry.Favourites);
            return Ok(favourites);
        }

        [Authorize]
        [HttpPost("addToWatchlist")]
        public async Task<IActionResult> AddToUserWatchlist([FromBody] List<int> mediaIds)
        {
            var newMedia = new List<Media>();
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var userEntry = await _context.Users.Include(user => user.WatchList).SingleOrDefaultAsync(user => user.Id == authUser.Id);
            foreach(int id in mediaIds){
                var mediaObj = await SearchHelper.SearchForSpecificTitle(_context,id);
                if(_context.Favourites.FirstOrDefault(media => media.Id == id) == null){
                    newMedia.Add(mediaObj);
                    userEntry.WatchList.Add(mediaObj);
                }
                else{
                    userEntry.WatchList.Add(mediaObj);
                }
            }
            await _context.WatchList.AddRangeAsync(newMedia);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("getWatchlist")]
        public async Task<IActionResult> GetUserWatchlist()
        {
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var userEntry = await _context.Users.Include(user => user.WatchList).SingleOrDefaultAsync(user => user.Id == authUser.Id);
            return Ok(MediaHelper.ConvertMediaIntoWatchListDto(userEntry.WatchList));
        }
    }
}