
using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;


        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAuthors();

            if (authors != null)
            {
                return Ok(authors);
            }
            else { return BadRequest(); }

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<UpdateAuthorDTO> GetAuthortByID(int id)
        {
            UpdateAuthorDTO dto = await _authorService.GetByID(id);
            return dto;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDTO model)
        {
            if (model is not null)
            {
                await _authorService.Register(model);
                return CreatedAtAction("GetAuthorByID", new { id = model.Id, Name = model.FirstName }, model);

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorDTO model)
        {
            if (model is not null)
            {
                await _authorService.Update(model);
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete]
        [Route("[action] /{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.Delete(id);
            return Ok("Author Silindi");
        }

        


    }
}
