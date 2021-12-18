using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using API.Model.DTOs;
using API.Model.Entities;
using API.Model.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public AdminAuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDto)
        {
            if(!registerDto.Email.Contains("@entertain.me")) return BadRequest("email not valid");
            var result = await _userManager.CreateAsync(
            new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName
            },
                registerDto.Password
            );
            if (result.Succeeded)
            {
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var authUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(!authUser.Email.Contains("@entertain.me")){
            return BadRequest();
            }

            if(authUser == null){
                return Unauthorized("Email doesn't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(authUser, loginDto.Password, false);

            if (result.Succeeded)
            {
                return Ok(new UserDTO{
                    Token = _tokenService.CreateToken(authUser)
                });
            }

            return Unauthorized("Password is not correct");
        }
    }
}