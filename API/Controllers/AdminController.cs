using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using API.Model.DTOs;
using API.Model.Entities;
using API.Model.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        private DataContext _context;
        private readonly UserManager<User> _userManager;
        
        public AdminController(DataContext context, UserManager<User> userManager){
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMedia(){

            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }


            return Ok(_context.Users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntryById(int id){

            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }

            // var mediaId = new Guid(id);
            var mediaItem = await _context.Favourites.FindAsync(id);
            return Ok(mediaItem);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddMedia(Media media){

            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }

            // get the user with the authenticated email
            var user = await _userManager.Users.Include(x => x.WatchList).FirstOrDefaultAsync(x => x.NormalizedEmail.Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            // var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).ToString());

            if (media == null)
                return BadRequest();

            // add media entries 
            await _context.Favourites.AddAsync(media);
            await _context.SaveChangesAsync();
            
            return Ok(media);
        }


        [Authorize]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMedia(int id,[FromBody] Media media){

            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }      

            var mediaobj = await _context.Favourites.SingleOrDefaultAsync(m => m.Id == id);

            mediaobj.Adult = media.Adult;
            mediaobj.MediaType = media.MediaType;
            mediaobj.Language = media.Language;
            mediaobj.Title = media.Title;
            mediaobj.Overview = media.Overview;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllMedia(){
            
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }

            foreach (var item in _context.Favourites)
            {
                _context.Favourites.Remove(item);
            }
            return Ok("It has been done...");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id){

            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }

            foreach (var item in _context.Favourites)
            {
                if (item.Id == id)
                {
                    _context.Favourites.Remove(item);
                }
            }
            return Ok();
        }
    }
}