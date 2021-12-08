using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        // Manage Media - [Admin]
        [HttpGet]
        public async Task<IActionResult> GetAllMedia(){
            return Ok();
        }

        // returns one entry
        [HttpGet("/getEntry")]
        public async Task<IActionResult> GetEntry(){
            return Ok();
        }

        // add media entries with a Creator and Genre 
        [HttpPost("/add")]
        public async Task<IActionResult> AddMedia(){
            return Ok();
        }

        // edit any and all attributes 
        [HttpPost("/edit")]
        public async Task<IActionResult> EditMedia(){
            return Ok();
        }

        // delete
        [HttpDelete("/deleteAll")]
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