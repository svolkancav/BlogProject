using BlogProject.Presentation.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BlogProject.Presentation.Controllers
{

    public class UserController : Controller
    {
        private readonly string _baseAddress = "https://localhost:7132";
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            LoginDTO loginDTO = new LoginDTO();
            return View(loginDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                LoginDTO model = new LoginDTO();
                model.UserName = loginDTO.UserName;
                model.Password = loginDTO.Password;

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/User/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonTokenContent = await response.Content.ReadAsStringAsync();

                    var dataToken = JsonSerializer.Deserialize<TokenDTO>(jsonTokenContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    HttpContext.Session.SetString("Token", dataToken.Token);

                    return RedirectToAction("Index", "Genre");
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            using (var client = new HttpClient())
            {
                RegisterDTO model = new RegisterDTO();
                model.UserName = registerDTO.UserName;
                model.Password = registerDTO.Password;
                model.Email = registerDTO.Email;
                client.BaseAddress = new Uri(_baseAddress);

                _configuration.GetSection("BlogProject.API").Bind(model);

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/User/Register", content);

                if (response.IsSuccessStatusCode) 
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

    }

}
