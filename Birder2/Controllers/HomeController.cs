using Birder2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Birder2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private IMemoryCache _cache;

        public HomeController(IConfiguration config, IMemoryCache memoryCache)
        {
            _config = config;
            _cache = memoryCache;
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
 
        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}