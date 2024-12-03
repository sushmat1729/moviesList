using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieList.Data;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }
        private readonly ILogger<HomeController> _logger;
        public HomeController(MovieContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();
            return View(movies);
        }
        //Removing Iaction methods
        public IActionResult Privacy()
        {
            return View();
        }

        

    }
}
