using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        // data injection
        public AdminController(DataContext context){
            _context = context;
        }

        // Manage Media - [Admin]
        [HttpGet]
        public IEnumerable<User> GetAllMedia(){
            return _context.Users;
        }

        // returns one entry
        [Authorize]
        [HttpGet("/getEntry/{id}")]
        public async Task<IActionResult> GetEntryById(string id){

            
            var userId = new Guid(id);
            var user = await _userManager.FindByIdAsync(id);
            return Ok();
        }

        // add media entries with a Creator and Genre 
        [HttpPost("/api/admin/add")]
        public async Task<IActionResult> AddMedia(){
            return Ok();
        }

        // edit any and all attributes 
        [HttpPost("/api/admin/edit")]
        public async Task<IActionResult> EditMedia(){
            return Ok();
        }

        // delete
        [HttpDelete("/api/admin/deleteAll")]
        public async Task<IActionResult> DeleteAllMedia(){
            


            return Ok();
        }

        // delete a specific entry
        [HttpDelete]
        public async Task<IActionResult> DeleteEntry(){
            return Ok();
        }

        // Add Media / Creator / Genre - add in add method

    }
}