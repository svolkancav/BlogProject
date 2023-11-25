using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUsers()
        {
            var users = _appUserService.GetUsers();

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

    }
}
