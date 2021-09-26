using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RafaelaColabora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // TODO: The model to be passed to the view and ultimately to the blazor component
            ApplicationUser aperson = new ApplicationUser()
            {
                FirstName = "Wael",
                LastName = "kdouh"
            };

            return View(aperson);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
