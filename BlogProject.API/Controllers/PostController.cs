using AutoMapper;
using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.PostServices;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace BlogProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }


        //DB de en az bir post olmalı yoksa çalışmaz.
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            if (posts != null)
            {
                return Ok(posts);
            }
            else { return BadRequest(); }

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<UpdatePostDTO> GetPostByID(int id)
        {
            UpdatePostDTO dto = await _postService.GetByID(id);
            return dto;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreatePost(CreatePostDTO model)
        {
            if (model is not null)
            {
                await _postService.Register(model);
                return CreatedAtAction("GetPostByID", new { id = model.Id, Title = model.Title}, model);

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePost(UpdatePostDTO model)
        {
            if (model is not null)
            {
                await _postService.Update(model);
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete]
        [Route("[action] /{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(id);
            return Ok("Post Silindi");
        }
    }
}
