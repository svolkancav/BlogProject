//using AutoMapper;
//using BlogProject.Application.Services.PostServices;
//using Microsoft.AspNetCore.Mvc;

//namespace BlogProject.API.Controllers
//{
//    [ApiController]
//    [Route("api/[Controller]")]
//    public class PostController : Controller
//    {
//        private readonly IPostService _postService;
//        private readonly IMapper _mapper;

//        public PostController(IPostService postService, IMapper mapper)
//        {
//            _postService = postService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [Route("[action]")]
//        public IActionResult GetPosts()
//        {
//            var posts = _postService.GetPosts();

//            if (posts != null)
//            {
//                return Ok(posts);
//            }
//            else { return BadRequest(); }

//        }
//    }
//}
