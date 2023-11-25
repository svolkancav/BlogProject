using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.GenreServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;


        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetGenres();
            if (genres != null)
            {
                return Ok(genres);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetGenreByID(int id)
        {
            var genre = await _genreService.GetByID(id);
            if (genre != null) 
            {
                return Ok(genre);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genreDTO)
        {
            await _genreService.Register(genreDTO);
            return CreatedAtAction("GetGenreByID", new { id= genreDTO.ID, name= genreDTO.Name },genreDTO);

        }

        [HttpPut]
        [Route("[action]")]

        public async Task<IActionResult> UpdateGenre([FromBody] GenreDTO genreDTO)
        {
            
            if (genreDTO != null)
            {
                await _genreService.Update(genreDTO);
                return Ok(genreDTO);
            }
            else { return BadRequest(); }
            
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteGenre (int id)
        {
            await _genreService.Delete(id);
            return Ok("Genre silindi!");


        }


    }
}
