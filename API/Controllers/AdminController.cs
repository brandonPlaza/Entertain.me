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
        public IEnumerable<User> GetAllMedia(){
            return _context.Users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntryById(int id){

            // var mediaId = new Guid(id);
            var mediaItem = await _context.Media.FindAsync(id);
            return Ok(mediaItem);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddMedia(Media media){

            // get the user with the authenticated email
            var user = await _userManager.Users.Include(x => x.WatchList).FirstOrDefaultAsync(x => x.NormalizedEmail.Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            // var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).ToString());

            if (media == null)
                return BadRequest();

            // add media entries 
            await _context.Media.AddAsync(media);
            await _context.SaveChangesAsync();
            
            return Ok(media);
        }


        // edit a media item (just the media item, without the genre table)
        [Authorize]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditMedia(int id,[FromBody] MediaDTO mediaDTO){

            // ??? add a new item to Media table that has mediaDTO's fields ?        

            // find the item and replace the fields with mediaDTO's fields 
            foreach (var item in _context.Media)
            {
                if (item.Id == id)
                {
                    item.Adult = mediaDTO.Adult;
                    item.MediaType = mediaDTO.MediaType;
                    item.Language = mediaDTO.Language;
                    item.Title = mediaDTO.Title;
                    item.Overview = mediaDTO.Overview;
                }
            }
            return Ok();
        }

        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllMedia(){
            

            foreach (var item in _context.Media)
            {
                _context.Media.Remove(item);
            }
            return Ok("It has been done...");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEntry(int id){

            foreach (var item in _context.Media)
            {
                if (item.Id == id)
                {
                    _context.Media.Remove(item);
                }
            }
            return Ok();
        }
    }
}