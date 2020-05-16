using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(IConfiguration config, IMapper mapper,
        UserManager<User> userManager, SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;

            this._mapper = mapper;
            
            this._config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
             userForRegister.username = userForRegister.username.ToLower();
            // if (await _repo.UserExists(userForRegister.username))
            //     return BadRequest("User already exists");

            var userToCreate = _mapper.Map<User>(userForRegister);
            var result= await _userManager.CreateAsync(userToCreate,userForRegister.password);
             var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);
            if(result.Succeeded){
            //var createdUser = await _repo.Register(userToCreate, userForRegister.password);
           
            return CreatedAtRoute("GetUser", new { Controller = "Users", id = userToCreate.Id }, userToReturn);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
           // var userFromRepo = await _repo.Login(userLoginDto.Username.ToLower(), userLoginDto.Password);
            var user= await _userManager.FindByNameAsync(userLoginDto.Username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
            // if (userFromRepo == null)
            //     return Unauthorized();
            if(result.Succeeded){
                    
                var appUser = _mapper.Map<UserForListDto>(user);
                return Ok(new
                {
                    token = await GenerateJwtToken(user),
                    user = appUser
                });

                

            }
            return Unauthorized();

        }

        public async Task<string> GenerateJwtToken(User userFromRepo)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var roles = await _userManager.GetRolesAsync(userFromRepo);

            foreach (var role in roles)
            {
                claims.Add( new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}