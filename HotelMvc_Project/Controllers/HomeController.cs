using System.Diagnostics;
using HotelMvc_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public IActionResult Error500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}
