using AutoMapper;
using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.AppUserService;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IAppUserService appUserService, IMapper mapper, IConfiguration configuration)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _appUserService.GetUsers();

            if (users != null)
            {
                return Ok(users);
            }
            else { return BadRequest(); }

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserByUserName(string name)
        {
            var user = await _appUserService.GetByUserName(name);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();

        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var user = await _appUserService.GetUserByID(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDTO registerDTO)
        {
            await _appUserService.Register(registerDTO);
            return CreatedAtAction("GetUserByID", new { UserName = registerDTO.UserName, Email = registerDTO.Email }, registerDTO);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateProfileDTO model)
        {
            if (model is not null)
            {
                await _appUserService.UpdateUser(model);
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]/{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _appUserService.DeleteUser(id);
            return Ok("User Silindi");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {

            var result = await _appUserService.Login(model);

            //Kullanici varsa JWT Token İşlemi Yapılır.
            if (result.Succeeded)
            {
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:secretKey"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JwtSettings:validIssuer"],
                _configuration["JwtSettings:validAudience"],
                authClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return token;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userDTO)
        {
            List<UpdateProfileDTO> tempUserDTO = await _appUserService.GetUsers();
            UpdateProfileDTO tempUser = tempUserDTO.SingleOrDefault(x=>x.Email == userDTO.Email);

            //var result = await _appUserService.Register(userDTO);

            AppUser result = _mapper.Map<AppUser>(tempUser);

            if (result == null)
            {
                await _appUserService.Register(userDTO);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
