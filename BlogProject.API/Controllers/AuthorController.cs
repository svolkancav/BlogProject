
using BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
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


    }
}
