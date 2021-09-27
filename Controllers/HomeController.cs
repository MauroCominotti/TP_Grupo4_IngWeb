using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RafaelaColabora.Data;
using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RafaelaColabora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: The model to be passed to the view and ultimately to the blazor component
            //ApplicationUser aperson = new ApplicationUser()
            //{
            //    FirstName = "Wael",
            //    LastName = "kdouh"
            //};
            //User.FindFirst(ClaimTypes.NameIdentifier).Value 
            //ViewData["FKUserId"] = (await _context.ApplicationUsers.FindAsync(ClaimTypes.NameIdentifier)).Id;
            ViewData["FKUserId"] = "1";
            //ViewData["FKCategoryId"] = "1";
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //ApplicationUser appUser = new ApplicationUser(){ Id = "1"; CategoryId = "1" };
            return View();
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
