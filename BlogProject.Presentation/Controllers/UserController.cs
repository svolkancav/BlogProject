﻿using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
