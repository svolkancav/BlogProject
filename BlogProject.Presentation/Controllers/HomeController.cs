using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Services.GenreServices;
using BlogProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenreService _genreService;

        public HomeController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _genreService.GetGenres());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreDTO model)
        {
            await _genreService.Register(model);
            return RedirectToAction("Index");
        }

    }
}