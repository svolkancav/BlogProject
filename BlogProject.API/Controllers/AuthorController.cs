
//using BlogProject.Application.Services.AuthorService;
//using Microsoft.AspNetCore.Mvc;

//namespace BlogProject.API.Controllers
//{
//    [ApiController]
//    [Route("api/[Controller]")]
//    public class AuthorController : Controller
//    {
//        private readonly IAuthorService _authorService;


//        public AuthorController(IAuthorService authorService)
//        {
//        }


//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        [Route("[action]")]
//        public IActionResult GetAuthors()
//        {
//            var authors = _authorService.GetAuthors();

//            if (authors != null)
//            {
//                return Ok(authors);
//            }
//            else { return BadRequest(); }

//        }


//    }
//}
