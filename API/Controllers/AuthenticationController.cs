using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.DTOs;
using API.Model.Entities;
using API.Model.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDto)
        {
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

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null){
                return Unauthorized("Email doesn't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return Ok(new UserDTO{
                    Token = _tokenService.CreateToken(user)
                });
            }

            return Unauthorized("Password is not correct");
        }
    }
}