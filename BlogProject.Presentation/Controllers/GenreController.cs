using BlogProject.Presentation.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BlogProject.Presentation.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly string _baseAddress = "https://localhost:7132";
        public ActionResult Index()
        {
            List<GenreVM> genreVMs;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);

                //Token i uygulama içi sayfalara verebilmek için
                string token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = client.GetAsync("api/Genre/GetAllGenres");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<List<GenreVM>>();
                    read.Wait();
                    genreVMs = read.Result;
                    return View(genreVMs);
                }
                else
                {
                    return NotFound();
                }

            }
        }
    }
}
